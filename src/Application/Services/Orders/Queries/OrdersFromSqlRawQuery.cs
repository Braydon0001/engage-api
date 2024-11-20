using Engage.Application.Services.Orders.Models;

namespace Engage.Application.Services.Orders.Queries;

public class OrderFromSqlRawQuery : GetQuery, IRequest<ListResult<OrderListDto>>
{
    public int EngageRegionId { get; set; }
    public int OrderStatusId { get; set; }
}

public class OrderFromSqlRawQueryHandler : BaseQueryHandler, IRequestHandler<OrderFromSqlRawQuery, ListResult<OrderListDto>>
{
    public OrderFromSqlRawQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<OrderListDto>> Handle(OrderFromSqlRawQuery query, CancellationToken cancellationToken)
    {
        var entities = await _context.Orders.FromSqlRaw("SELECT * FROM engageensource2.vw_orders o WHERE o.EngageRegionId = {0} and o.OrderStatusId = {1}", query.EngageRegionId, query.OrderStatusId)
                                            .IgnoreQueryFilters()
                                            .Select(e => new OrderListDto
                                            {
                                                Id = e.OrderId,
                                                OrderTypeId = e.OrderTypeId,
                                                OrderStatusId = e.OrderStatusId,
                                                OrderDate = e.OrderDate,
                                                Deleted = e.Deleted,
                                                DeletedBy = e.DeletedBy,
                                                DeletedDate = e.DeletedDate,
                                                DeliveryDate = e.DeliveryDate,
                                                DistributionCenterId = e.DistributionCenterId,
                                                StoreId = e.StoreId
                                            })

                                           .ToListAsync(cancellationToken);

        return new ListResult<OrderListDto>(entities);
    }
}
