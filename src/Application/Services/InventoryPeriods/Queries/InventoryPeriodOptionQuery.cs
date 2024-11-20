namespace Engage.Application.Services.InventoryPeriods.Queries;

public class InventoryPeriodOptionQuery : IRequest<List<InventoryPeriodOption>>
{
    public int InventoryYearId { get; init; }
}

public record InventoryPeriodOptionListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryPeriodOptionQuery, List<InventoryPeriodOption>>
{
    public async Task<List<InventoryPeriodOption>> Handle(InventoryPeriodOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.InventoryPeriods.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.InventoryYearId == query.InventoryYearId);

        return await queryable.OrderBy(e => e.InventoryYearId)
                              .ThenBy(e => e.Number)
                              .ProjectTo<InventoryPeriodOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}