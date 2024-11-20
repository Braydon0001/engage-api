namespace Engage.Application.Services.SupplierSubRegions.Queries;

public class SupplierSubRegionListQuery : IRequest<List<SupplierSubRegionDto>>
{
    // Query property. Used for filtering.
    public int SupplierRegionId { get; init; }
}

public record SupplierSubRegionListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SupplierSubRegionListQuery, List<SupplierSubRegionDto>>
{
    public async Task<List<SupplierSubRegionDto>> Handle(SupplierSubRegionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SupplierSubRegions.AsQueryable().AsNoTracking();

        // Filter the queryable
        queryable = queryable.Where(e => e.SupplierRegionId == query.SupplierRegionId);

        return await queryable.OrderBy(e => e.SupplierRegionId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<SupplierSubRegionDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}