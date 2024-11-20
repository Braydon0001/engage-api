namespace Engage.Application.Services.RoleUserGroups.Queries;

public class RoleUserGroupListQuery : IRequest<List<RoleUserGroupDto>>
{

}

public record RoleUserGroupListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<RoleUserGroupListQuery, List<RoleUserGroupDto>>
{
    public async Task<List<RoleUserGroupDto>> Handle(RoleUserGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.RoleUserGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.RoleUserGroupId)
                              .ProjectTo<RoleUserGroupDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}