namespace Engage.Application.Services.RoleUserGroups.Queries;

public record RoleUserGroupVmQuery(int Id) : IRequest<RoleUserGroupVm>;

public record RoleUserGroupVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<RoleUserGroupVmQuery, RoleUserGroupVm>
{
    public async Task<RoleUserGroupVm> Handle(RoleUserGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.RoleUserGroups.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Role)
                             .Include(e => e.UserGroup);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.RoleUserGroupId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<RoleUserGroupVm>(entity);
    }
}