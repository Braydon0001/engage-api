namespace Engage.Application.Services.UserOrganizationRoles.Queries;

public record UserOrganizationRoleVmQuery(int Id) : IRequest<UserOrganizationRoleVm>;

public record UserOrganizationRoleVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserOrganizationRoleVmQuery, UserOrganizationRoleVm>
{
    public async Task<UserOrganizationRoleVm> Handle(UserOrganizationRoleVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserOrganizationRoles.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.User)
                             .Include(e => e.UserOrganization)
                             .Include(e => e.UserRole);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.UserOrganizationRoleId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<UserOrganizationRoleVm>(entity);
    }
}