namespace Engage.Application.Services.RoleUserGroups.Queries;

public class RoleUserGroupOptionQuery : IRequest<List<RoleUserGroupOption>>
{ 

}

public record RoleUserGroupOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<RoleUserGroupOptionQuery, List<RoleUserGroupOption>>
{
    public async Task<List<RoleUserGroupOption>> Handle(RoleUserGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.RoleUserGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.RoleUserGroupId)
                              .ProjectTo<RoleUserGroupOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}