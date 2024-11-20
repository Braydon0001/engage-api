namespace Engage.Application.Services.UserOrganizations.Queries;

public record UserOrganizationVmQuery(int Id) : IRequest<UserOrganizationVm>;

public record UserOrganizationVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserOrganizationVmQuery, UserOrganizationVm>
{
    public async Task<UserOrganizationVm> Handle(UserOrganizationVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserOrganizations.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Supplier);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.UserOrganizationId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<UserOrganizationVm>(entity);
    }
}