namespace Engage.Application.Services.SecurityRoles.Queries;

public record SecurityRoleVmQuery(int Id) : IRequest<SecurityRoleVm>;

public record SecurityRoleVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityRoleVmQuery, SecurityRoleVm>
{
    public async Task<SecurityRoleVm> Handle(SecurityRoleVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SecurityRoles.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SecurityPermissionRoles)
                             .ThenInclude(e => e.SecurityPermission);

        var entity = await queryable.SingleOrDefaultAsync(e => e.SecurityRoleId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SecurityRoleVm>(entity);
    }
}