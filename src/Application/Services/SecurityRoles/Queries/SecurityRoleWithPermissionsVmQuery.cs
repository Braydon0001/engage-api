namespace Engage.Application.Services.SecurityRoles.Queries;

public class SecurityRoleWithPermissionsVmQuery : IRequest<SecurityRoleWithPermissionsVm>
{
    public int Id { get; set; }
}
public record SecurityRoleWithPermissionsVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityRoleWithPermissionsVmQuery, SecurityRoleWithPermissionsVm>
{
    public async Task<SecurityRoleWithPermissionsVm> Handle(SecurityRoleWithPermissionsVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SecurityRoles.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SecurityPermissionRoles)
        .ThenInclude(e => e.SecurityPermission);

        var entity = await queryable.SingleOrDefaultAsync(e => e.SecurityRoleId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SecurityRoleWithPermissionsVm>(entity);
        throw new NotImplementedException();
    }
}