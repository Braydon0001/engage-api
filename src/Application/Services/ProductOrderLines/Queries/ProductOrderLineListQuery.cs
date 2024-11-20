using Engage.Application.Services.ProductPeriods.Queries;

namespace Engage.Application.Services.ProductOrderLines.Queries;

public class ProductOrderLineListQuery : IRequest<List<ProductOrderLineDto>>
{
    public int ProductOrderId { get; set; }
}

public record ProductOrderLineListHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProductOrderLineListQuery, List<ProductOrderLineDto>>
{
    public async Task<List<ProductOrderLineDto>> Handle(ProductOrderLineListQuery query, CancellationToken cancellationToken)
    {
        var order = await Context.ProductOrders
                                 .Where(e => e.ProductOrderId == query.ProductOrderId)
                                 .FirstOrDefaultAsync(cancellationToken);

        var queryable = Context.ProductOrderLines.AsQueryable().AsNoTracking().Where(e => e.ProductOrderId == query.ProductOrderId);

        var entities = await queryable.OrderBy(e => e.ProductOrderLineId)
                              .ProjectTo<ProductOrderLineDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        if (order.ProductOrderTypeId == (int)ProductOrderTypeEnum.Transfer)
        {
            var periodIds = await Mediator.Send(new ProductPeriodCurrentPreviousIdQuery());

            var productIds = entities.Select(e => e.ProductId).ToList();

            var warehouseSummary = await Context.ProductWarehouseSummaries
                                                .AsNoTracking()
                                                .Where(e => e.ProductWarehouseId == order.ProductWarehouseOutId
                                                    && productIds.Contains(e.ProductId)
                                                    && e.ProductPeriodId == periodIds.PreviousPeriodId)
                                                .ToListAsync(cancellationToken);

            var transactions = await Context.ProductTransactions
                                            .AsNoTracking()
                                            .Where(e => productIds.Contains(e.ProductId)
                                                && e.ProductWarehouseId == order.ProductWarehouseOutId
                                                && e.ProductPeriodId == periodIds.CurrentPeriodId)
                                            .ToListAsync(cancellationToken);

            foreach (var product in entities)
            {
                //var priorStock = warehouseSummary.Where(e => e.ProductId == product.ProductId).Select(e => e.Quantity).FirstOrDefault(0f);
                product.StockInWarehouse = warehouseSummary.Where(e => e.ProductId == product.ProductId).Select(e => e.Quantity).FirstOrDefault(0f)
                            + transactions.Where(e => e.ProductId == product.ProductId).Select(e => e.Quantity).ToList().Sum();
            }

        }

        return entities;
    }
}