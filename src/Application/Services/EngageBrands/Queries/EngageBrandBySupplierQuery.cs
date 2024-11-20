namespace Engage.Application.Services.EngageBrands.Queries;

public class EngageBrandBySupplierQuery : IRequest<List<EngageBrandOption>>
{
    public string SupplierIds { get; set; }

}

public record EngageBrandBySupplierHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageBrandBySupplierQuery, List<EngageBrandOption>>
{
    public async Task<List<EngageBrandOption>> Handle(EngageBrandBySupplierQuery query, CancellationToken cancellationToken)
    {
        List<EngageBrandOption> result = [];

        if (query.SupplierIds.IsNotNullOrWhiteSpace())
        {
            List<int> supplierIds = query.SupplierIds.Split(',').Select(int.Parse).ToList();

            var supplierBrands = await Context.SupplierEngageBrands
                                              .Where(e => supplierIds.Contains(e.SupplierId))
                                              .Include(e => e.EngageBrand)
                                              .Select(e => e.EngageBrand)
                                              .ProjectTo<EngageBrandOption>(Mapper.ConfigurationProvider)
                                              .ToListAsync();
            var mappedEntities = supplierBrands.DistinctBy(e => e.Id);
            result.AddRange(mappedEntities);
        }

        return result;
    }
}
