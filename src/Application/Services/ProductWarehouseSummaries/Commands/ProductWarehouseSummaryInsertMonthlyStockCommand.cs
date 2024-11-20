using Engage.Application.Services.ProductWarehouses.Queries;

namespace Engage.Application.Services.ProductWarehouseSummaries.Commands;

public class ProductWarehouseSummaryInsertMonthlyStockCommand : IRequest<OperationStatus>
{
}
public class ProductWarehouseSummaryInserMonthlyStockHandler(IAppDbContext context, IMapper mapper) : InsertHandler(context, mapper), IRequestHandler<ProductWarehouseSummaryInsertMonthlyStockCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProductWarehouseSummaryInsertMonthlyStockCommand request, CancellationToken cancellationToken)
    {

        var currentDate = DateTime.Now;
        //if previous day was last day in a period
        var previousPeriod = await _context.ProductPeriods.Where(e => e.EndDate.Date == currentDate.AddDays(-1).Date)
                                                      .FirstOrDefaultAsync(cancellationToken);

        List<ProductWarehouseSummary> currentPeriodStock = [];

        if (previousPeriod != null)
        {
            var date = previousPeriod.StartDate.AddDays(-2);
            //last period that had stock on hand
            var stockOnHandPeriod = await _context.ProductPeriods.Where(e => e.StartDate.Date <= date.Date && e.EndDate.Date >= date.Date)
                                                                 .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("no stockOnHandPeriod found");

            var warehouses = await _context.ProductWarehouses
                                           .ProjectTo<ProductWarehouseDto>(_mapper.ConfigurationProvider)
                                           .ToListAsync(cancellationToken);

            foreach (var warehouse in warehouses)
            {


                var warehouseTransactions = await _context.ProductTransactions
                                                              .Where(e => e.ProductPeriodId == previousPeriod.ProductPeriodId
                                                                        && e.ProductWarehouseId == warehouse.Id)
                                                              .ToListAsync(cancellationToken);

                List<int> productIds = new();

                //get closing balance from prior period
                var closingBalances = await _context.ProductWarehouseSummaries
                                                        .Where(e => e.ProductWarehouseId == warehouse.Id
                                                            && e.ProductPeriodId == stockOnHandPeriod.ProductPeriodId)
                                                        .ToListAsync(cancellationToken);

                foreach (var product in closingBalances)
                {

                    var transactions = warehouseTransactions.Where(e => e.ProductId == product.ProductId)
                                                            .ToList();

                    currentPeriodStock.Add(new ProductWarehouseSummary
                    {
                        ProductWarehouseId = product.ProductWarehouseId,
                        ProductPeriodId = previousPeriod.ProductPeriodId,
                        ProductId = product.ProductId,
                        Quantity = transactions.Sum(x => x.Quantity) + product.Quantity
                    });

                    productIds.Add(product.ProductId);
                }

                var newProductTransactions = warehouseTransactions.Where(e => !productIds.Contains(e.ProductId))
                                                       .ToList();

                if (newProductTransactions.Count > 0)
                {
                    var newProductIds = newProductTransactions.DistinctBy(e => e.ProductId).Select(e => e.ProductId).ToList();

                    foreach (var productId in newProductIds)
                    {
                        var transactions = newProductTransactions.Where(e => e.ProductId == productId).ToList();

                        currentPeriodStock.Add(new ProductWarehouseSummary
                        {
                            ProductId = productId,
                            ProductPeriodId = previousPeriod.ProductPeriodId,
                            ProductWarehouseId = warehouse.Id,
                            Quantity = transactions.Sum(x => x.Quantity)
                        });
                    }
                }

            }

            _context.ProductWarehouseSummaries.AddRange(currentPeriodStock);

            await _context.SaveChangesAsync(cancellationToken);
        }

        return new OperationStatus(true);
    }
}