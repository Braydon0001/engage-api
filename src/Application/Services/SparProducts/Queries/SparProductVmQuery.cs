namespace Engage.Application.Services.SparProducts.Queries;

public record SparProductVmQuery(int Id) : IRequest<SparProductVm>;

public record SparProductVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparProductVmQuery, SparProductVm>
{
    public async Task<SparProductVm> Handle(SparProductVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparProducts.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SparUnitType)
                             .Include(e => e.EngageBrand)
                             .Include(e => e.Supplier)
                             .Include(e => e.EngageSubCategory)
                             .ThenInclude(e => e.EngageCategory)
                             .ThenInclude(e => e.EngageSubGroup)
                             .ThenInclude(e => e.EngageGroup)
                             .Include(e => e.SparProductStatus)
                             .Include(e => e.SparAnalysisGroup)
                             .Include(e => e.SparSystemStatus)
                             .Include(e => e.EvoLedger);

        var entity = await queryable.SingleOrDefaultAsync(e => e.SparProductId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SparProductVm>(entity);
    }
}