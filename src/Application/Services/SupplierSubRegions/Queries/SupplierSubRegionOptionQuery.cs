namespace Engage.Application.Services.SupplierSubRegions.Queries;

public class SupplierSubRegionOptionQuery : IRequest<List<SupplierSubRegionOption>>
{
    public int? SupplierId { get; init; }
    public int? SupplierRegionId { get; init; }
}

public record SupplierSubRegionOptionListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SupplierSubRegionOptionQuery, List<SupplierSubRegionOption>>
{
    public async Task<List<SupplierSubRegionOption>> Handle(SupplierSubRegionOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SupplierSubRegions.AsQueryable().AsNoTracking();

        if (query.SupplierId.HasValue)
        {
            queryable = queryable.Where(e => e.SupplierRegion.SupplierId == query.SupplierId);
        }
        
        if (query.SupplierRegionId.HasValue)
        {
            queryable = queryable.Where(e => e.SupplierRegionId == query.SupplierRegionId);
        }        

        return await queryable.OrderBy(e => e.SupplierRegionId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<SupplierSubRegionOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}