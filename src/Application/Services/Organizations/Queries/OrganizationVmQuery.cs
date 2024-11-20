namespace Engage.Application.Services.Organizations.Queries;

public record OrganizationVmQuery(int Id) : IRequest<OrganizationVm>;

public record OrganizationVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrganizationVmQuery, OrganizationVm>
{
    public async Task<OrganizationVm> Handle(OrganizationVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Organizations.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.OrganizationId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<OrganizationVm>(entity);
    }
}