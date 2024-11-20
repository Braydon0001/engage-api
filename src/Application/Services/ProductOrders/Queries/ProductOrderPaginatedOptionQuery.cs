namespace Engage.Application.Services.ProductOrders.Queries;

public class ProductOrderPaginatedOptionQuery : PaginatedQuery, IRequest<List<ProductOrderOption>>
{
}

public record ProductOrderPaginatedOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderPaginatedOptionQuery, List<ProductOrderOption>>
{
    public async Task<List<ProductOrderOption>> Handle(ProductOrderPaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = ProductOrderPaginationProps.Create();

        var queryable = Context.ProductOrders.AsQueryable().AsNoTracking();

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<ProductOrderOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }    
}


