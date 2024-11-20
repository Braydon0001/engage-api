namespace Engage.Application.Services.UserRolePermissions.Queries;

public class UserRolePermissionOptionQuery : IRequest<List<UserRolePermissionOption>>
{ 

}

public record UserRolePermissionOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserRolePermissionOptionQuery, List<UserRolePermissionOption>>
{
    public async Task<List<UserRolePermissionOption>> Handle(UserRolePermissionOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserRolePermissions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.UserRolePermissionId)
                              .ProjectTo<UserRolePermissionOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}