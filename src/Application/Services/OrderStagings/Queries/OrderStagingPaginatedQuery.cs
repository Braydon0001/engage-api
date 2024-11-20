namespace Engage.Application.Services.OrderStagings.Queries;

public class OrderStagingPaginatedQuery : PaginatedQuery, IRequest<ListResult<OrderStagingDto>>
{
}

public record OrderStagingPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrderStagingPaginatedQuery, ListResult<OrderStagingDto>>
{
    public async Task<ListResult<OrderStagingDto>> Handle(OrderStagingPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = OrderStagingPaginationProps.Create();

        var queryable = Context.OrderStagings.AsQueryable().AsNoTracking();

        var entities = await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<OrderStagingDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        return new(entities);
    }
}


