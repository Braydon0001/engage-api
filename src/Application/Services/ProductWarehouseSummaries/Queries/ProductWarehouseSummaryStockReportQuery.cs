using Engage.Application.Services.ProductWarehouses.Queries;

namespace Engage.Application.Services.ProductWarehouseSummaries.Queries;

public class ProductWarehouseSummaryStockReportQuery : IRequest<List<ProductWarehouseSummaryStockReportDto>>
{
    public List<int> ProductWarehouseIds { get; set; }
}
public class ProductWarehouseSummaryStockReportHandler : ListQueryHandler, IRequestHandler<ProductWarehouseSummaryStockReportQuery, List<ProductWarehouseSummaryStockReportDto>>
{
    private readonly ContactReportSettings _contactReportSettings;
    public ProductWarehouseSummaryStockReportHandler(IAppDbContext context, IMapper mapper, IOptions<ContactReportSettings> contactReportSettings) : base(context, mapper)
    {
        _contactReportSettings = contactReportSettings.Value;
    }

    public async Task<List<ProductWarehouseSummaryStockReportDto>> Handle(ProductWarehouseSummaryStockReportQuery request, CancellationToken cancellationToken)
    {
        var warehouseQuery = _context.ProductWarehouses.AsNoTracking().AsQueryable();

        if (request.ProductWarehouseIds != null && request.ProductWarehouseIds.Any())
        {
            warehouseQuery = warehouseQuery.Where(e => request.ProductWarehouseIds.Contains(e.ProductWarehouseId));
        }
        var warehouses = await warehouseQuery
                                    .ProjectTo<ProductWarehouseDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);

        var currentDate = DateTime.Now;

        var currentPeriod = await _context.ProductPeriods
                                          .AsNoTracking()
                                          .Where(e => e.StartDate.Date <= currentDate.Date && e.EndDate.Date >= currentDate.Date)
                                          .FirstOrDefaultAsync(cancellationToken);

        if (currentPeriod == null)
            throw new Exception("No current period found");

        var previousPeriod = await _context.ProductPeriods
                                           .AsNoTracking()
                                           .Where(e => e.EndDate >= currentPeriod.StartDate.AddDays(-1)
                                                && e.StartDate <= currentPeriod.StartDate.AddDays(-1))
                                           .FirstOrDefaultAsync(cancellationToken);

        if (previousPeriod == null)
            throw new Exception("No past period found");

        List<ProductWarehouseSummaryStockReportDto> entity = new();

        foreach (var warehouse in warehouses)
        {
            //get current stock
            var stockOnHand = await _context.ProductWarehouseSummaries
                                            .AsNoTracking()
                                            .Where(e => e.ProductWarehouseId == warehouse.Id
                                                && e.ProductPeriodId == previousPeriod.ProductPeriodId)
                                            .ProjectTo<ProductWarehouseSummaryStockReportStockDto>(_mapper.ConfigurationProvider)
                                            .ToListAsync(cancellationToken);

            ProductWarehouseSummaryStockReportDto currentWarehouse = new()
            {
                ProductWarehouseId = warehouse.Id,
                ProductWarehouseName = warehouse.Name,
                EngageLogo = _contactReportSettings.EngageLogo,
                Products = new()
            };

            var transactions = await _context.ProductTransactions
                                             .AsNoTracking()
                                             .Where(e => e.ProductPeriodId == currentPeriod.ProductPeriodId
                                                && e.ProductWarehouseId == warehouse.Id)
                                             .ProjectTo<ProductWarehouseSummaryStockReportTransactionDto>(_mapper.ConfigurationProvider)
                                             .ToListAsync(cancellationToken);

            var transactionGrouped = transactions.GroupBy(e => e.ProductId).ToDictionary(e => e.Key, e => e.ToList());

            foreach (var transaction in transactionGrouped)
            {

                var currentStock = stockOnHand.Where(e => e.ProductId == transaction.Key).FirstOrDefault();
                var stockAmount = currentStock != null ? currentStock.Quantity + transaction.Value.Sum(e => e.Quantity) : transaction.Value.Sum(e => e.Quantity);
                if (currentStock != null)
                {
                    stockOnHand.Remove(currentStock);
                }

                currentWarehouse.Products.Add(new ProductWarehouseSummaryStockReportProductDto
                {
                    ProductId = transaction.Key,
                    ProductName = transaction.Value[0].ProductName, //minimum of one entry
                    ProductMasterId = transaction.Value[0].ProductMasterId,
                    ProductMasterName = transaction.Value[0].ProductMasterName,
                    CurrentStock = stockAmount
                });
            }

            foreach (var stockItem in stockOnHand)
            {
                currentWarehouse.Products.Add(new ProductWarehouseSummaryStockReportProductDto
                {
                    ProductId = stockItem.ProductId,
                    ProductName = stockItem.ProductName,
                    ProductMasterId = stockItem.ProductMasterId,
                    ProductMasterName = stockItem.ProductMasterName,
                    CurrentStock = stockItem.Quantity
                });
            }

            entity.Add(currentWarehouse);
        }

        return entity;

    }
}