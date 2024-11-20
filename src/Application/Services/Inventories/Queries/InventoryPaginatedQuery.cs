namespace Engage.Application.Services.Inventories.Queries;

public class InventoryPaginatedQuery : PaginatedQuery, IRequest<List<InventoryDto>>
{
}

public record InventoryPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryPaginatedQuery, List<InventoryDto>>
{
    public async Task<List<InventoryDto>> Handle(InventoryPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = InventoryPaginationProps.Create();

        var queryable = Context.Inventories.AsQueryable().AsNoTracking();

        #region Custom Sort 
        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.InventoryId);
        }
        #endregion

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<InventoryDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


