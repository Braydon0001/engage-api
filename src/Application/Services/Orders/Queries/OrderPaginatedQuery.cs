using Engage.Application.Services.Orders.Models;

namespace Engage.Application.Services.Orders.Queries;

public class OrderPaginatedQuery : PaginatedQuery, IRequest<ListResult<OrderListDto>>
{
}

public record OrderPaginatedHandler(IAppDbContext Context, IMapper Mapper, IUserService User) : IRequestHandler<OrderPaginatedQuery, ListResult<OrderListDto>>
{

    public async Task<ListResult<OrderListDto>> Handle(OrderPaginatedQuery query, CancellationToken cancellationToken)
    {
        var paginationProps = OrderPaginationProps.Create();

        var queryable = Context.Orders.AsQueryable().AsNoTracking();

        #region Custom Filters
        if (!User.IsHostSupplier)
        {
            queryable = queryable.Where(e => e.SupplierId == User.SupplierId);
        }

        #endregion

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.OrderId);
        }

        var entities = await queryable.Filter(query, paginationProps)
                                      .Sort(query, paginationProps)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<OrderListDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}
