namespace Engage.Application.Services.Inventories.Queries;

public class InventoryPaginatedOptionQuery : PaginatedQuery, IRequest<List<InventoryOption>>
{
}

public record InventoryPaginatedOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryPaginatedOptionQuery, List<InventoryOption>>
{
    public async Task<List<InventoryOption>> Handle(InventoryPaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = InventoryPaginationProps.Create();

        var queryable = Context.Inventories.AsQueryable().AsNoTracking();

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<InventoryOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }    
}


