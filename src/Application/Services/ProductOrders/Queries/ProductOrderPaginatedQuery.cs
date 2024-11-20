namespace Engage.Application.Services.ProductOrders.Queries;

public class ProductOrderPaginatedQuery : PaginatedQuery, IRequest<List<ProductOrderDto>>
{
}

public record ProductOrderPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderPaginatedQuery, List<ProductOrderDto>>
{
    public async Task<List<ProductOrderDto>> Handle(ProductOrderPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = ProductOrderPaginationProps.Create();

        var queryable = Context.ProductOrders.AsQueryable().AsNoTracking();

        if (query.SortModel.Count == 0)
        {
            queryable = queryable.OrderByDescending(e => e.ProductOrderId);
        }

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<ProductOrderDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


