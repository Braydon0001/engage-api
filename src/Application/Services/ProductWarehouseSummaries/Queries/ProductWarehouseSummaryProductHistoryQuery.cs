using Engage.Application.Services.ProductTransactions.Queries;

namespace Engage.Application.Services.ProductWarehouseSummaries.Queries;

public class ProductWarehouseSummaryProductHistoryQuery : IRequest<ProductWarehouseSummaryProductHistoryDto>
{
    public int ProductWarehouseId { get; set; }
    public int ProductId { get; set; }
}
public class ProductWarehouseSummaryProductHistoryHandler : ListQueryHandler, IRequestHandler<ProductWarehouseSummaryProductHistoryQuery, ProductWarehouseSummaryProductHistoryDto>
{
    public ProductWarehouseSummaryProductHistoryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductWarehouseSummaryProductHistoryDto> Handle(ProductWarehouseSummaryProductHistoryQuery query, CancellationToken cancellationToken)
    {
        var product = await _context.Products.AsNoTracking()
            .Where(e => e.ProductId == query.ProductId)
            .FirstOrDefaultAsync(cancellationToken);

        var warehouse = await _context.ProductWarehouses.AsNoTracking()
            .Where(e => e.ProductWarehouseId == query.ProductWarehouseId)
            .FirstOrDefaultAsync(cancellationToken);

        var stockOnHand = await _context.ProductWarehouseSummaries.AsNoTracking()
                                                                  .Where(e => e.ProductWarehouseId == query.ProductWarehouseId && e.ProductId == query.ProductId)
                                                                  .OrderBy(e => e.ProductPeriodId)
                                                                  .ToListAsync(cancellationToken);

        List<ProductWarehouseSummaryProductHistoryStock> entities = new();

        int index = 0;
        foreach (var stockPeriod in stockOnHand)
        {
            var transactions = await _context.ProductTransactions.AsNoTracking()
                                                                 .Where(e => e.ProductId == query.ProductId && e.ProductWarehouseId == query.ProductWarehouseId && e.ProductPeriodId == stockPeriod.ProductPeriodId)
                                                                 .ProjectTo<ProductTransactionByProductWarehouseDto>(_mapper.ConfigurationProvider)
                                                                 .ToListAsync(cancellationToken);

            var period = await _context.ProductPeriods.AsNoTracking()
                                                      .Where(e => e.ProductPeriodId == stockPeriod.ProductPeriodId)
                                                      .FirstOrDefaultAsync(cancellationToken);

            entities.Add(new ProductWarehouseSummaryProductHistoryStock
            {
                Index = index,
                ProductPeriodName = period.Name,
                StockOpen = index - 1 < 0 ? 0 : stockOnHand[index - 1].Quantity,
                StockClose = stockPeriod.Quantity,
                Transactions = transactions,
                IsCurrentPeriod = false
            });
            index++;
        }

        //get current period's stock details
        var currentPeriod = await _context.ProductPeriods.AsNoTracking()
                                                      .Where(e => e.StartDate.Date <= DateTime.Now.Date && e.EndDate.Date >= DateTime.Now.Date)
                                                      .FirstOrDefaultAsync(cancellationToken);

        var currentTransactions = await _context.ProductTransactions.AsNoTracking()
                                                                    .Where(e => e.ProductPeriodId == currentPeriod.ProductPeriodId
                                                                    && e.ProductId == query.ProductId && e.ProductWarehouseId == query.ProductWarehouseId)
                                                                    .ProjectTo<ProductTransactionByProductWarehouseDto>(_mapper.ConfigurationProvider)
                                                                    .ToListAsync(cancellationToken);

        ProductWarehouseSummaryProductHistoryStock currentPeriodStock = new ProductWarehouseSummaryProductHistoryStock
        {
            Index = index,
            ProductPeriodName = currentPeriod.Name,
            StockOpen = stockOnHand.Count > 0 ? stockOnHand.Last().Quantity : 0,
            Transactions = currentTransactions,
            IsCurrentPeriod = true
        };

        entities.Add(currentPeriodStock);

        return new ProductWarehouseSummaryProductHistoryDto
        {
            ProductId = product.ProductId,
            ProductName = product.Name,
            ProductWarehouseId = warehouse.ProductWarehouseId,
            ProductWarehouseName = warehouse.Name,
            TransactionHistory = entities,
            CurrentBalance = currentTransactions.Sum(e => e.Quantity) + currentPeriodStock.StockOpen
        };
    }
}