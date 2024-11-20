using Finbuckle.MultiTenant.Abstractions;
using Okta.Sdk;
using Okta.Sdk.Configuration;

namespace Engage.Application.Services.Users.Commands;

public class UpdateUserGroupsCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public List<int> Permissions { get; set; }
    public bool? SkipOktaActions { get; set; } = true;
}

public class UpdateUserGroupsCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateUserGroupsCommand, OperationStatus>
{
    private readonly IOptions<JwtOptions> _options;
    private readonly IMultiTenantContextAccessor _multiTenantContext;
    public UpdateUserGroupsCommandHandler(IAppDbContext context, IMapper mapper, IOptions<JwtOptions> options, IMultiTenantContextAccessor multiTenantContext) : base(context, mapper)
    {
        _options = options;
        _multiTenantContext = multiTenantContext;
    }

    public async Task<OperationStatus> Handle(UpdateUserGroupsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(x => x.UserId == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException("User", request.Id);
        }

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

            oktaUserId = user.Id;

            await user.UpdateAsync(cancellationToken);
        }

        if (request.Permissions != null)
        {
            if (request.Permissions.Count != 0)
            {
                var userGroups = await _context.UserUserGroups.IgnoreQueryFilters().Where(e => e.UserId == request.Id && e.UserGroup.Name != "Everyone" && !e.Disabled && !e.Deleted).ToListAsync(cancellationToken); //get all teh user's current groups except the 'everyone' group

                if (!userGroups.Any()) //if the user has no groups, add all the ones that came in the request
                {
                    foreach (var groupId in request.Permissions)
                    {
                        var groupVendorId = _context.UserGroups.IgnoreQueryFilters().Where(e => e.UserGroupId == groupId && !e.Disabled && !e.Deleted).Select(e => e.VendorId).FirstOrDefault();
                        if (groupVendorId != null)
                        {
                            if (request.SkipOktaActions == false && oktaUserId != "")
                            {
                                await client.Groups.AddUserToGroupAsync(groupVendorId, oktaUserId, cancellationToken); //add User to Okta Groups
                            }
                            var userUserGroup = new UserUserGroup //add user to userUserGroups table
                            {
                                UserId = entity.UserId,
                                UserGroupId = groupId
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
                    var groupsToRemove = userGroups.Where(e => !request.Permissions.Contains(e.UserGroupId)).ToList();
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

                    var groupsToAdd = request.Permissions.Where(e => !userGroups.Select(e => e.UserGroupId).Contains(e)).ToList();
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
