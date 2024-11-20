namespace Engage.Application.Services.UserPermissions.Queries;

public class UserPermissionOptionQuery : IRequest<List<UserPermissionOption>>
{ 

}

public record UserPermissionOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserPermissionOptionQuery, List<UserPermissionOption>>
{
    public async Task<List<UserPermissionOption>> Handle(UserPermissionOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserPermissions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.UserPermissionId)
                              .ProjectTo<UserPermissionOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}