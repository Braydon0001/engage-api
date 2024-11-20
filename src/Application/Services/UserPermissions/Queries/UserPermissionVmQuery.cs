namespace Engage.Application.Services.UserPermissions.Queries;

public record UserPermissionVmQuery(int Id) : IRequest<UserPermissionVm>;

public record UserPermissionVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserPermissionVmQuery, UserPermissionVm>
{
    public async Task<UserPermissionVm> Handle(UserPermissionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserPermissions.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.UserPermissionId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<UserPermissionVm>(entity);
    }
}