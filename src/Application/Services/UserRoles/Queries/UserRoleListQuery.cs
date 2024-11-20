namespace Engage.Application.Services.UserRoles.Queries;

public class UserRoleListQuery : IRequest<List<UserRoleDto>>
{

}

public record UserRoleListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserRoleListQuery, List<UserRoleDto>>
{
    public async Task<List<UserRoleDto>> Handle(UserRoleListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserRoles.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.UserRoleId)
                              .ProjectTo<UserRoleDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}