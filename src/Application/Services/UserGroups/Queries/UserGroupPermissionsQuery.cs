using Engage.Application.Services.UserGroups.Models;

namespace Engage.Application.Services.UserGroups.Queries;

public class UserGroupPermissionsQuery : IRequest<List<UserGroupPermission>>
{
    public int? UserId { get; set; }
}

public record UserGroupPermissionsHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserGroupPermissionsQuery, List<UserGroupPermission>>
{
    public async Task<List<UserGroupPermission>> Handle(UserGroupPermissionsQuery request, CancellationToken cancellationToken)
    {
        var user = await Context.Users.FirstOrDefaultAsync(e => e.UserId == request.UserId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("User", request.UserId);
        }

        //get all the user groups
        var queryable = Context.UserGroups.IgnoreQueryFilters().Where(e => !e.Deleted && !e.Deleted && e.Name != "Everyone").AsQueryable();

        //get all the groups the user belongs to
        var userGroupIds = await Context.UserUserGroups.Where(e => e.UserId == request.UserId).Select(e => e.UserGroupId).ToListAsync(cancellationToken);

        //get all the groups assigned by the user's role
        var userRoleGroupIds = await Context.RoleUserGroups.Where(e => e.RoleId == user.RoleId).Select(e => e.UserGroupId).ToListAsync(cancellationToken);

        var allGroups = await queryable.ProjectTo<UserGroupPermission>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken);

        var entities = allGroups.Select(e =>
        {
            e.Assigned = userGroupIds.Contains(e.Id) || userRoleGroupIds.Contains(e.Id);
            e.FromRole = userRoleGroupIds.Contains(e.Id);
            return e;
        }).ToList();

        return entities;
    }
}
