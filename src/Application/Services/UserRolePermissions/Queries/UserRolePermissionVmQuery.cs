namespace Engage.Application.Services.UserRolePermissions.Queries;

public record UserRolePermissionVmQuery(int Id) : IRequest<UserRolePermissionVm>;

public record UserRolePermissionVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserRolePermissionVmQuery, UserRolePermissionVm>
{
    public async Task<UserRolePermissionVm> Handle(UserRolePermissionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserRolePermissions.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.UserRole)
                             .Include(e => e.UserPermission);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.UserRolePermissionId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<UserRolePermissionVm>(entity);
    }
}