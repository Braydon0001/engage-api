using Finbuckle.MultiTenant.Abstractions;
using Okta.Sdk;
using Okta.Sdk.Configuration;

namespace Engage.Application.Services.Users.Commands;

public class UpdateUserCommand : UserCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public List<int> Groups { get; set; }
    public List<int> EngageSubGroupIds { get; set; }
    public List<int> CommunicationTypeIds { get; set; }
    public bool IsEmployeeEmailUpdate { get; set; } = true;
}

public class UpdateUserCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateUserCommand, OperationStatus>
{
    private readonly IOptions<JwtOptions> _options;
    private readonly IMultiTenantContextAccessor _multiTenantContext;
    public UpdateUserCommandHandler(IAppDbContext context, IMapper mapper, IOptions<JwtOptions> options, IMultiTenantContextAccessor multiTenantContext) : base(context, mapper)
    {
        _options = options;
        _multiTenantContext = multiTenantContext;
    }

    public async Task<OperationStatus> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.SingleAsync(x => x.UserId == request.Id, cancellationToken);

        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.EmailAddress1 == entity.Email, cancellationToken);

        if (employee != null)
        {
            employee.UserId = request.Id;
            if (request.IsEmployeeEmailUpdate)
            {
                employee.EmailAddress1 = request.Email;
            }
        }

        var userEngageSubGroups = await _context.UserEngageSubGroups.Where(e => e.UserId == request.Id).ToListAsync(cancellationToken);
        if (request.EngageSubGroupIds != null && request.EngageSubGroupIds.Count > 0)
        {
            var engageSubGroupIds = userEngageSubGroups.Select(e => e.EngageSubGroupId).ToList();
            var subGroupsToRemove = userEngageSubGroups.Where(e => !request.EngageSubGroupIds.Contains(e.EngageSubGroupId)).ToList();
            if (subGroupsToRemove.Count > 0)
            {
                _context.UserEngageSubGroups.RemoveRange(subGroupsToRemove);
            }

            var subGroupsToAdd = request.EngageSubGroupIds.Where(e => !engageSubGroupIds.Contains(e)).ToList();
            if (subGroupsToAdd.Count > 0)
            {
                foreach (var subGroupId in subGroupsToAdd)
                {
                    var userEngageSubGroup = new UserEngageSubGroup
                    {
                        UserId = request.Id,
                        EngageSubGroupId = subGroupId
                    };
                    _context.UserEngageSubGroups.Add(userEngageSubGroup);
                }
            }
        }
        else
        {
            if (userEngageSubGroups.Count > 0)
            {
                _context.UserEngageSubGroups.RemoveRange(userEngageSubGroups);
            }
        }

        var userCommunicationTypes = await _context.UserCommunicationTypes.Where(e => e.UserId == request.Id).ToListAsync(cancellationToken);
        if (request.CommunicationTypeIds != null && request.CommunicationTypeIds.Count > 0)
        {
            var communicationTypeIds = userCommunicationTypes.Select(e => e.CommunicationTypeId).ToList();
            var communicationTypesToRemove = userCommunicationTypes.Where(e => !request.CommunicationTypeIds.Contains(e.CommunicationTypeId)).ToList();
            if (communicationTypesToRemove.Count > 0)
            {
                _context.UserCommunicationTypes.RemoveRange(communicationTypesToRemove);
            }

            var communicationTypesToAdd = request.CommunicationTypeIds.Where(e => !communicationTypeIds.Contains(e)).ToList();
            if (communicationTypesToAdd.Count > 0)
            {
                foreach (var communicationTypeId in communicationTypesToAdd)
                {
                    var userCommunicationType = new UserCommunicationType
                    {
                        UserId = request.Id,
                        CommunicationTypeId = communicationTypeId,
                    };
                    _context.UserCommunicationTypes.Add(userCommunicationType);
                }
            }
        }
        else
        {
            if (userCommunicationTypes.Count > 0)
            {
                _context.UserCommunicationTypes.RemoveRange(userCommunicationTypes);
            }
        }

        var userRegions = await _context.UserRegions.Where(e => e.UserId == request.Id).ToListAsync(cancellationToken);
        if (request.EngageRegionIds != null && request.EngageRegionIds.Count > 0)
        {
            var regionIds = userRegions.Select(e => e.EngageRegionId).ToList();
            var regionsToRemove = userRegions.Where(e => !request.EngageRegionIds.Contains(e.EngageRegionId)).ToList();
            if (regionsToRemove.Count > 0)
            {
                _context.UserRegions.RemoveRange(regionsToRemove);
            }

            var regionsToAdd = request.EngageRegionIds.Where(e => !regionIds.Contains(e)).ToList();
            if (regionsToAdd.Count > 0)
            {
                foreach (var regionId in regionsToAdd)
                {
                    var userRegion = new UserRegion
                    {
                        UserId = request.Id,
                        EngageRegionId = regionId,
                    };
                    _context.UserRegions.Add(userRegion);
                }
            }
        }
        else
        {
            if (userRegions.Count > 0)
            {
                _context.UserRegions.RemoveRange(userRegions);
            }
        }

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
            { 278, "sparencore" },
            { 275, "accelerate" },
        };

        var client = new OktaClient(new OktaClientConfiguration
        {
            OktaDomain = _options.Value.Authority,
            Token = _options.Value.UsersApiToken
        });

        var oktaUserId = "";

        if (request.SkipOktaActions == false)
        {
            var tenantIdentifier = _multiTenantContext.MultiTenantContext.TenantInfo.Identifier;

            var user = await client.Users.GetUserAsync(entity.Email, cancellationToken);
            user.Profile = new UserProfile
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Login = request.Email,
                MobilePhone = request.MobilePhone,
                Organization = tenantIdentifier ?? "engage",
            };

            var supplierSetting = await _context.SupplierSettings.Where(s => s.SupplierId == request.SupplierId && s.SettingId == (int)SupplierSettingId.OktaSupplier)
                                                                 .FirstOrDefaultAsync(cancellationToken);

            if (supplierSetting != null)
            {
                user.Profile["supplier"] = supplierSetting.Value;
            }
            else
            {
                if (suppliers.TryGetValue(request.SupplierId, out var supplier))
                {
                    user.Profile["supplier"] = supplier;
                }
            }

            oktaUserId = user.Id;

            await user.UpdateAsync(cancellationToken);
        }

        _mapper.Map(request, entity);

        //manual mapping because automapper does not want to map the RoleId
        entity.RoleId = request.RoleId;

        if (request.Groups != null)
        {
            if (request.Groups.Count != 0)
            {
                var userGroups = await _context.UserUserGroups.IgnoreQueryFilters().Where(e => e.UserId == request.Id && e.UserGroup.Name != "Everyone" && !e.Disabled && !e.Deleted).ToListAsync(cancellationToken); //get all teh user's current groups except the 'everyone' group

                if (!userGroups.Any()) //if the user has no groups, add all the ones that came in the request
                {
                    foreach (var group in request.Groups)
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
                else
                {
                    var groupsToRemove = userGroups.Where(e => !request.Groups.Contains(e.UserGroupId)).ToList();
                    if (groupsToRemove.Count > 0)
                    {
                        foreach (var group in groupsToRemove)
                        {
                            var userGroup = _context.UserGroups.IgnoreQueryFilters().FirstOrDefault(e => e.UserGroupId == group.UserGroupId && !e.Disabled && !e.Deleted);
                            if (request.SkipOktaActions == false && oktaUserId != "")
                            {
                                await client.Groups.RemoveUserFromGroupAsync(userGroup.VendorId, oktaUserId, cancellationToken);
                            }
                        }
                        _context.UserUserGroups.RemoveRange(groupsToRemove);
                    }

                    var groupsToAdd = request.Groups.Where(e => !userGroups.Select(e => e.UserGroupId).Contains(e)).ToList();
                    if (groupsToAdd.Count > 0)
                    {
                        foreach (var group in groupsToAdd)
                        {
                            var dbGroup = _context.UserGroups.IgnoreQueryFilters().FirstOrDefault(e => e.UserGroupId == group && !e.Disabled && !e.Disabled);
                            if (request.SkipOktaActions == false && oktaUserId != "")
                            {
                                await client.Groups.AddUserToGroupAsync(dbGroup.VendorId, oktaUserId, cancellationToken);
                            }
                            var groupToAdd = new UserUserGroup
                            {
                                UserGroupId = dbGroup.UserGroupId,
                                UserId = entity.UserId,
                            };
                            _context.UserUserGroups.Add(groupToAdd);
                        }
                    }

                }

            }
            else
            {
                var userGroups = await _context.UserUserGroups.IgnoreQueryFilters().Where(e => e.UserId == request.Id && e.UserGroup.Name != "Everyone" && !e.Disabled && !e.Deleted).ToListAsync();
                if (userGroups.Count > 0)
                {
                    foreach (var group in userGroups)
                    {
                        var userGroup = _context.UserGroups.IgnoreQueryFilters().FirstOrDefault(e => e.UserGroupId == group.UserGroupId && !e.Disabled && !e.Disabled);
                        if (request.SkipOktaActions == false && oktaUserId != "")
                        {
                            await client.Groups.RemoveUserFromGroupAsync(userGroup.VendorId, oktaUserId, cancellationToken);
                        }
                    }
                    _context.UserUserGroups.RemoveRange(userGroups);
                }
            }
        }
        else
        {
            var userGroups = await _context.UserUserGroups.IgnoreQueryFilters().Where(e => e.UserId == request.Id && e.UserGroup.Name != "Everyone" && !e.Disabled && !e.Deleted).ToListAsync(cancellationToken);
            if (userGroups.Count > 0)
            {
                foreach (var group in userGroups)
                {
                    var userGroup = _context.UserGroups.IgnoreQueryFilters().FirstOrDefault(e => e.UserGroupId == group.UserGroupId && !e.Disabled && !e.Disabled);
                    if (request.SkipOktaActions == false && oktaUserId != "")
                    {
                        await client.Groups.RemoveUserFromGroupAsync(userGroup.VendorId, oktaUserId, cancellationToken);
                    }
                }
                _context.UserUserGroups.RemoveRange(userGroups);
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.UserId;
        return opStatus;
    }
}
