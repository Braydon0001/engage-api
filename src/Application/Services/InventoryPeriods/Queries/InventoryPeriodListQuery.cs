namespace Engage.Application.Services.InventoryPeriods.Queries;

public class InventoryPeriodListQuery : IRequest<List<InventoryPeriodDto>>
{
    // Query property. Used for filtering.
    public int InventoryYearId { get; init; }
}

public record InventoryPeriodListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryPeriodListQuery, List<InventoryPeriodDto>>
{
    public async Task<List<InventoryPeriodDto>> Handle(InventoryPeriodListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.InventoryPeriods.AsQueryable().AsNoTracking();

        // Filter the queryable
        queryable = queryable.Where(e => e.InventoryYearId == query.InventoryYearId);

        return await queryable.OrderBy(e => e.InventoryYearId)
                              .ThenBy(e => e.Number)
                              .ProjectTo<InventoryPeriodDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}