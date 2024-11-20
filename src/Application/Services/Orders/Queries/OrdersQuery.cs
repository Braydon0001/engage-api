using Engage.Application.Services.Orders.Models;

namespace Engage.Application.Services.Orders.Queries;

public class OrdersQuery : GetQuery, IRequest<ListResult<OrderSubTotalDto>>
{
    public int EngageRegionId { get; set; }
    public int OrderStatusId { get; set; }
}

public class OrdersQueryHandler : BaseQueryHandler, IRequestHandler<OrdersQuery, ListResult<OrderSubTotalDto>>
{
    public OrdersQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<OrderSubTotalDto>> Handle(OrdersQuery query, CancellationToken cancellationToken)
    {
        var entities = await _context.Orders.AsNoTracking().IgnoreQueryFilters()
                                            .Where(x => x.OrderStatusId == query.OrderStatusId &&
                                                        x.Store.EngageRegionId == query.EngageRegionId
                                                        && x.Deleted == false && x.Disabled == false)
                                            .OrderByDescending(x => x.OrderId)
                                            .ProjectTo<OrderSubTotalDto>(_mapper.ConfigurationProvider)
                                            .ToListAsync(cancellationToken);

        // Exclude orders with no products
        entities = entities.Where(x => x.QuantitySum > 0).ToList();

        return new ListResult<OrderSubTotalDto>(entities);
    }
}