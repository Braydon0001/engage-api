namespace Engage.Application.Services.ProductTransactions.Queries;

public class ProductTransactionPaginatedQuery : PaginatedQuery, IRequest<ListResult<ProductTransactionDto>>
{
}

public record ProductTransactionPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductTransactionPaginatedQuery, ListResult<ProductTransactionDto>>
{
    public async Task<ListResult<ProductTransactionDto>> Handle(ProductTransactionPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = ProductTransactionPaginationProps.Create();

        var queryable = Context.ProductTransactions.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.ProductTransactionId);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<ProductTransactionDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}


