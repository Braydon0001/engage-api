using Engage.Application.Services.Employees.Commands;
using Finbuckle.MultiTenant.Abstractions;
using Okta.Sdk;
using Okta.Sdk.Configuration;
using User = Engage.Domain.Entities.User;

namespace Engage.Application.Services.Users.Commands;

public class CreateUserCommand : UserCommand, IRequest<OperationStatus>
{
    public List<int> Groups { get; set; }
    public List<int> EngageSubGroupIds { get; set; }
    public List<int> CommunicationTypeIds { get; set; }
    public bool CreateEmployee { get; set; } = true;
}

public class CreateUserCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateUserCommand, OperationStatus>
{
    private readonly IOptions<JwtOptions> _options;
    private readonly IMultiTenantContextAccessor _multiTenantContext;
    public CreateUserCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IOptions<JwtOptions> options, IMultiTenantContextAccessor multiTenantContext) : base(context, mapper, mediator)
    {
        _options = options;
        _multiTenantContext = multiTenantContext;
    }

    public async Task<OperationStatus> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateUserCommand, User>(request);

        if (request.EngageSubGroupIds != null && request.EngageSubGroupIds.Count > 0)
        {
            foreach (var engageSubGroupId in request.EngageSubGroupIds)
            {
                var userEngageSubGroup = new UserEngageSubGroup
                {
                    User = entity,
                    EngageSubGroupId = engageSubGroupId,
                };
                _context.UserEngageSubGroups.Add(userEngageSubGroup);
            }
        }

        if (request.CommunicationTypeIds != null && request.CommunicationTypeIds.Count > 0)
        {
            foreach (var communicationTypeId in request.CommunicationTypeIds)
            {
                var userCommunicationType = new UserCommunicationType
                {
                    CommunicationTypeId = communicationTypeId,
                    User = entity,
                };

                _context.UserCommunicationTypes.Add(userCommunicationType);
            }
        }

        if (request.EngageRegionIds != null && request.EngageRegionIds.Count > 0)
        {
            foreach (var regionId in request.EngageRegionIds)
            {
                var userRegion = new UserRegion
                {
                    EngageRegionId = regionId,
                    User = entity,
                };

                _context.UserRegions.Add(userRegion);
            }
        }

        var client = new OktaClient(new OktaClientConfiguration
        {
            OktaDomain = _options.Value.Authority,
            Token = _options.Value.UsersApiToken
        });

        var oktaUserId = "";

        if (request.SkipOktaActions == false)
        {
            var suppliers = new Dictionary<int, string>()
        {
            { 18, "distell" },
            { 49, "heineken" },
            { 59, "encore" },
            { 97, "signalhill" },
            { 126, "engage" },
            { 230,"halewood" },
            { 236, "chipkins" },
            { 237,"stetsons" },
            { 238,"j&m" },
            { 263,"vegeworth" },
            { 1326, "unbxd" },
            { 275, "accelerate" },
            { 278, "sparencore" }
        };

            var tenantIdentifier = _multiTenantContext.MultiTenantContext.TenantInfo.Identifier;

            var user = await client.Users.CreateUserAsync(new CreateUserWithoutCredentialsOptions
            {
                Profile = new UserProfile
                {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Email = entity.Email,
                    Login = entity.Email,
                    MobilePhone = entity.MobilePhone,
                    Organization = tenantIdentifier ?? "engage",
                },
                Activate = false,
            }, cancellationToken);

            var supplierSetting = await _context.SupplierSettings.Where(s => s.SupplierId == request.SupplierId && s.SettingId == (int)SupplierSettingId.OktaSupplier)
                                                                 .FirstOrDefaultAsync(cancellationToken);

            if (supplierSetting != null)
            {
                user.Profile["supplier"] = supplierSetting.Value;  //add the supplier custom profile field
                if (supplierSetting.Value == "unbxd")
                {
                    user.Profile.Organization = "sparzw";
                }
                await user.UpdateAsync(cancellationToken);
            }
            else
            {
                if (suppliers.TryGetValue(entity.SupplierId, out var supplier))
                {
                    user.Profile["supplier"] = supplier; //add the supplier custom profile field
                    if (supplier == "unbxd")
                    {
                        user.Profile.Organization = "sparzw";
                    }
                    await user.UpdateAsync(cancellationToken);
                }
            }

            oktaUserId = user.Id;

            await user.ActivateAsync(true, cancellationToken);
        }

        _context.Users.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.UserId;

        var userUserGroupEntry = new UserUserGroup //Add the user to the 'Everyone' group in the DB to mirror Okta.
        {
            UserId = entity.UserId,
            UserGroupId = await _context.UserGroups.IgnoreQueryFilters().Where(e => e.Name == "Everyone" && !e.Disabled && !e.Deleted).Select(e => e.UserGroupId).SingleAsync(cancellationToken)
        };

        _context.UserUserGroups.Add(userUserGroupEntry);
        await _context.SaveChangesAsync(cancellationToken);

        if (request.Groups != null) //Groups will be null if the user is being created from the employee creation process
        {
            if (request.Groups.Count != 0) //Groups will have no entries if they are created in the groups screen with no groups selected.
            {
                foreach (var group in request.Groups) //if there are any groups chosen, add each entry into the DB to match Okta.
                {
                    var groupId = _context.UserGroups.IgnoreQueryFilters().Where(e => e.UserGroupId == group && !e.Disabled && !e.Deleted).Select(e => e.VendorId).FirstOrDefault();
                    if (groupId != null)
                    {
                        if (request.SkipOktaActions == false && oktaUserId != "")
                        {
                            await client.Groups.AddUserToGroupAsync(groupId, oktaUserId, cancellationToken); //add User to Okta Groups
                        }
                        var userUserGroup = new UserUserGroup //add user to userUserGroups table
                        {
                            UserId = entity.UserId,
                            UserGroupId = group
                        };
                        _context.UserUserGroups.Add(userUserGroup);
                    }
                    else
                    {
                        throw new Exception("Okta Group Not Found");
                    }
                }
            }
        }
        await _context.SaveChangesAsync(cancellationToken);

        var employee = await _context.Employees.Where(e => e.Disabled == false && e.Deleted == false)
                                               .SingleOrDefaultAsync(e => e.EmailAddress1 == entity.Email, cancellationToken);
        if (employee != null)
        {
            employee.UserId = entity.UserId;
            await _context.SaveChangesAsync(cancellationToken);
        }
        else if (request.CreateEmployee == false)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            //var lastEmployeeUserCode = await _context.Employees.Where(e => e.EmployeeTypeId == (int)EmployeeTypeId.User)
            //                                                   .OrderByDescending(e => e.EmployeeId)
            //                                                   .Select(e => e.Code)
            //                                                   .FirstOrDefaultAsync(cancellationToken);

            await _mediator.Send(new CreateEmployeeCommand
            {
                SkipUserCreation = true,
                EmployeeTypeId = (int)EmployeeTypeId.User,
                Code = "User-" + entity.UserId, //GenerateNextCode(lastEmployeeUserCode),
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = DateTime.Now,
                EmailAddress1 = entity.Email,
                StartingDate = DateTime.Now,
                UserId = entity.UserId,
                EmployeeGenderId = 1,
                EmployeeRaceId = 1,
                EmployeeTitleId = 1,
            }, cancellationToken);
        }

        return opStatus;
    }

    public string GenerateNextCode(string previousCode)
    {
        if (string.IsNullOrEmpty(previousCode))
        {
            return "User-1";
        }

        string[] parts = previousCode.Split('-');
        if (parts.Length == 2 && int.TryParse(parts[1], out int number))
        {
            return $"{parts[0]}-{number + 1}";
        }

        return "User-1";
    }

}
