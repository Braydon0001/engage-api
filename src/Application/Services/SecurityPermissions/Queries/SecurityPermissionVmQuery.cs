namespace Engage.Application.Services.SecurityPermissions.Queries;

public record SecurityPermissionVmQuery(int Id) : IRequest<SecurityPermissionVm>;

public record SecurityPermissionVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityPermissionVmQuery, SecurityPermissionVm>
{
    public async Task<SecurityPermissionVm> Handle(SecurityPermissionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SecurityPermissions.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.SecurityPermissionId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SecurityPermissionVm>(entity);
    }
}