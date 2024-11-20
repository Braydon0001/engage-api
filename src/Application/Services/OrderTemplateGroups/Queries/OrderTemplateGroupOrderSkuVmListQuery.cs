using Engage.Application.Services.OrderSkus.Queries;

namespace Engage.Application.Services.OrderTemplateGroups.Queries;

public class OrderTemplateGroupOrderSkuVmListQuery : IRequest<List<OrderTemplateGroupOrderSkuVm>>
{
    public int OrderId { get; set; }
}

public class OrderTemplateGroupOrderSkuVmListHandler : ListQueryHandler, IRequestHandler<OrderTemplateGroupOrderSkuVmListQuery, List<OrderTemplateGroupOrderSkuVm>>
{
    public OrderTemplateGroupOrderSkuVmListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OrderTemplateGroupOrderSkuVm>> Handle(OrderTemplateGroupOrderSkuVmListQuery query, CancellationToken cancellationToken)
    {
        // queryables
        var orderQueryable = _context.Orders.AsQueryable().AsNoTracking().IgnoreQueryFilters();
        var orderSkusQueryble = _context.OrderSkus.AsQueryable().AsNoTracking();
        var orderTemplateGroupsQueryable = _context.OrderTemplateGroups.AsQueryable().AsNoTracking();

        // entities
        var order = await orderQueryable.SingleAsync(e => e.OrderId == query.OrderId, cancellationToken);

        var orderSkus = await orderSkusQueryble.Where(e => e.OrderId == query.OrderId)
                                               .ProjectTo<OrderSkuProductVm>(_mapper.ConfigurationProvider)
                                               .ToListAsync(cancellationToken);

        var orderTemplateGroups = await orderTemplateGroupsQueryable.Include(e => e.OrderTemplateProducts)
                                                                    .Where(e => e.OrderTemplateId == order.OrderTemplateId)
                                                                    .ProjectTo<OrderTemplateGroupOrderSkuVm>(_mapper.ConfigurationProvider)
                                                                    .ToListAsync(cancellationToken);

        var orderTemplateProducts = orderTemplateGroups.SelectMany(e => e.OrderTemplateProducts).ToList();

        // Groups with products 
        //var groups = new List<OrderTemplateGroupOrderSkuVm>();
        foreach (var group in orderTemplateGroups)
        {
            var productIds = group.OrderTemplateProducts.Select(e => e.Id).ToList();
            var skus = orderSkus.Where(e => productIds.Contains(e.OrderTemplateProductId.Value)).ToList();

            foreach (var sku in skus)
            {
                var product = orderTemplateProducts.Single(e => e.Id == sku.OrderTemplateProductId);
                sku.Price = product.Price;
                sku.PromotionPrice = product.PromotionPrice;
                sku.RecommendedPrice = product.RecommendedPrice;
                sku.GrossProfitPercent = product.GrossProfitPercent;
                sku.Suffix = product.Suffix;
            }

            group.OrderSkus = skus;

            //groups.Add(group);
        }

        return orderTemplateGroups;
    }
}