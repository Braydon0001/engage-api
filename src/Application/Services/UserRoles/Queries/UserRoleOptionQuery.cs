namespace Engage.Application.Services.UserRoles.Queries;

public class UserRoleOptionQuery : IRequest<List<UserRoleOption>>
{ 

}

public record UserRoleOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserRoleOptionQuery, List<UserRoleOption>>
{
    public async Task<List<UserRoleOption>> Handle(UserRoleOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserRoles.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.UserRoleId)
                              .ProjectTo<UserRoleOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}