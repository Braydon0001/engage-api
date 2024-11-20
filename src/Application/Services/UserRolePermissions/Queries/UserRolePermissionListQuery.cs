namespace Engage.Application.Services.UserRolePermissions.Queries;

public class UserRolePermissionListQuery : IRequest<List<UserRolePermissionDto>>
{

}

public record UserRolePermissionListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserRolePermissionListQuery, List<UserRolePermissionDto>>
{
    public async Task<List<UserRolePermissionDto>> Handle(UserRolePermissionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserRolePermissions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.UserRolePermissionId)
                              .ProjectTo<UserRolePermissionDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}