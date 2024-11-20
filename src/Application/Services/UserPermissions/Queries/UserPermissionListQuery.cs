namespace Engage.Application.Services.UserPermissions.Queries;

public class UserPermissionListQuery : IRequest<List<UserPermissionDto>>
{

}

public record UserPermissionListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserPermissionListQuery, List<UserPermissionDto>>
{
    public async Task<List<UserPermissionDto>> Handle(UserPermissionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserPermissions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.UserPermissionId)
                              .ProjectTo<UserPermissionDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}