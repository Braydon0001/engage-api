using Engage.Application.Services.OrderSkus.Models;

namespace Engage.Application.Services.OrderSkus.Queries
{
    public class OrderSkuListQuery : GetQuery, IRequest<ListResult<OrderSkuListItemDto>>
    {
        public string OrderIds { get; set; }
        public int? OrderId { get; set; }
    }

    public class GetOrderSkuListQueryHandler : BaseQueryHandler, IRequestHandler<OrderSkuListQuery, ListResult<OrderSkuListItemDto>>
    {

        public GetOrderSkuListQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<OrderSkuListItemDto>> Handle(OrderSkuListQuery request, CancellationToken cancellationToken)
        {
            List<OrderSkuListItemDto> entities;

            if (!string.IsNullOrWhiteSpace(request.OrderIds))
            {
                var ids = new List<int>();
                foreach (var orderId in request.OrderIds.Split(","))
                {
                    if (int.TryParse(orderId, out int i))
                    {
                        ids.Add(i);
                    }
                }

                if (ids.Count > 0)
                {
                    entities = await _context.OrderSkus.IgnoreQueryFilters()
                                                       .Where(e => ids.Contains(e.OrderId) &&
                                                                   e.Deleted == false && e.Disabled == false)
                                                       .ProjectTo<OrderSkuListItemDto>(_mapper.ConfigurationProvider)
                                                       .ToListAsync(cancellationToken);

                    var ordersWithoutSkus = await _context.Orders
                                  .Where(e => ids.Contains(e.OrderId) &&
                                              e.OrderSkus.Count == 0)
                                  .Include(e => e.OrderType)
                                  .Include(e => e.OrderStatus)
                                  .Include(e => e.Store)
                                        .ThenInclude(e => e.EngageRegion)
                                  .Include(e => e.DistributionCenter)
                                  .Include(e => e.OrderEngageDepartments)
                                        .ThenInclude(e => e.EngageDepartment)
                                  .Select(e => MapOrderToSku(e))
                                  .ToListAsync(cancellationToken);

                    if (ordersWithoutSkus != null && ordersWithoutSkus.Count > 0)
                    {
                        entities.AddRange(ordersWithoutSkus);
                    }
                }
                else
                {
                    entities = new List<OrderSkuListItemDto>();
                }
            }
            else if (request.OrderId.HasValue)
            {
                entities = await _context.OrderSkus.IgnoreQueryFilters()
                                                   .Where(e => e.OrderId == request.OrderId &&
                                                               e.Deleted == false && e.Disabled == false)
                                                   .ProjectTo<OrderSkuListItemDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

                var orderWithoutSkus = await _context.Orders
                                   .Where(e => e.OrderId == request.OrderId &&
                                               e.OrderSkus.Count == 0)
                                   .Include(e => e.OrderType)
                                   .Include(e => e.OrderStatus)
                                   .Include(e => e.Store)
                                        .ThenInclude(e => e.EngageRegion)
                                   .Include(e => e.DistributionCenter)
                                   .Include(e => e.OrderEngageDepartments)
                                        .ThenInclude(e => e.EngageDepartment)
                                   .Select(e => MapOrderToSku(e))
                                   .ToListAsync(cancellationToken);

                if (orderWithoutSkus != null && orderWithoutSkus.Count > 0)
                {
                    entities.AddRange(orderWithoutSkus);
                }
            }
            else
            {
                entities = await _context.OrderSkus
                                   .ProjectTo<OrderSkuListItemDto>(_mapper.ConfigurationProvider)
                                   .ToListAsync(cancellationToken);
            }

            var vm = new ListResult<OrderSkuListItemDto>
            {
                Data = entities,
                Count = entities.Count
            };

            return vm;
        }

        private static OrderSkuListItemDto MapOrderToSku(Order order)
        {
            return new OrderSkuListItemDto()
            {
                OrderId = order.OrderId,
                OrderTypeId = order.OrderTypeId,
                OrderTypeName = order.OrderType.Name,
                OrderStatusId = order.OrderStatusId,
                OrderStatusName = order.OrderStatus.Name,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                SubmittedDate = order.SubmittedDate,
                ProcessedDate = order.ProcessedDate,
                OrderReference = order.OrderReference,
                StoreId = order.StoreId,
                StoreCode = order.Store.Code,
                StoreName = order.Store.Name,
                EngageRegionId = order.Store.EngageRegionId,
                EngageRegionName = order.Store.EngageRegion.Name,
                DistributionCenterId = order.DistributionCenterId,
                DistributionCenterCode = order.DistributionCenter.Code,
                DistributionCenterName = order.DistributionCenter.Name,
                AccountNumber = order.DCAccountNo,
                UserName = order.CreatedBy,
                OrderDepartments = string.Join(", ", order.OrderEngageDepartments.Select(e => e.EngageDepartment.Name))
            };
        }
    }
}
