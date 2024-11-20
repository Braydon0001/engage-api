using Engage.Application.Services.ProductTransactions.Queries;
using Engage.Application.Services.ProductWarehouses.Queries;

namespace Engage.Application.Services.ProductWarehouseSummaries.Queries;

public class ProductWarehouseSummaryStockOnHandTreeQuery : PaginatedQuery, IRequest<List<ProductWarehouseSummaryStockOnHandTreeDto>>
{
    public int ProductYearId { get; set; }
}
public class ProductWarehouseSummaryStockOnHandTreeHandler : BaseQueryHandler, IRequestHandler<ProductWarehouseSummaryStockOnHandTreeQuery, List<ProductWarehouseSummaryStockOnHandTreeDto>>
{
    public ProductWarehouseSummaryStockOnHandTreeHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductWarehouseSummaryStockOnHandTreeDto>> Handle(ProductWarehouseSummaryStockOnHandTreeQuery query, CancellationToken cancellationToken)
    {

        #region Current & Past Period

        var currentDate = DateTime.Now;

        var currentPeriod = await _context.ProductPeriods
                                          .AsNoTracking()
                                          .Where(e => e.StartDate.Date <= currentDate.Date
                                            && e.EndDate.Date >= currentDate.Date)
                                          .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("No current period found");

        bool isCurrentYear = currentPeriod.ProductYearId == query.ProductYearId;

        var periods = await _context.ProductPeriods
            .AsNoTracking()
            .Where(e => e.ProductYearId == query.ProductYearId)
            .OrderBy(e => e.EndDate)
            .ToListAsync(cancellationToken);

        var periodIds = periods.Select(e => e.ProductPeriodId).ToList();

        var previousPeriod = await _context.ProductPeriods
                                           .AsNoTracking()
                                           .Where(e => e.EndDate >= currentPeriod.StartDate.AddDays(-1)
                                                && e.StartDate <= currentPeriod.StartDate.AddDays(-1))
                                           .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("No previous period found");

        if (currentPeriod.ProductYearId == query.ProductYearId)
        {
            periods = periods.Where(e => e.EndDate < currentPeriod.StartDate).ToList();
        }
        else if (periods.Any())
        {
            //set currentPeriod to the last period of the selected year
            currentPeriod = periods.Last();
        }
        else
        {
            throw new Exception("No periods found for selected year");
        }

        #endregion

        // Level 0
        if (query.GroupKeys.IsNullOrEmpty())
        {
            List<ProductWarehouseSummaryStockOnHandTreeDto> entities = [];
            var warehouses = await _context.ProductWarehouses
                                           .AsNoTracking()
                                           .ProjectTo<ProductWarehouseDto>(_mapper.ConfigurationProvider)
                                           .ToListAsync(cancellationToken);

            var parentWarehouses = warehouses.Where(e => e.ParentId == null).ToList();

            var closingStock = await _context.ProductWarehouseSummaries
                                             .AsNoTracking()
                                             .Where(e => e.ProductPeriodId == previousPeriod.ProductPeriodId)
                                             .ProjectTo<ProductWarehouseSummaryDto>(_mapper.ConfigurationProvider)
                                             .ToListAsync(cancellationToken);

            var transactions = await _context.ProductTransactions
                                             .AsNoTracking()
                                             .Where(e => e.ProductPeriodId == currentPeriod.ProductPeriodId)
                                             .ToListAsync(cancellationToken);

            foreach (var warehouse in parentWarehouses)
            {
                float totalStock = 0;

                var childWarehouseIds = warehouses.Where(e => e.ParentId == warehouse.Id).Select(e => e.Id).ToList();

                var productTransactions = transactions
                                            .Where(e => childWarehouseIds.Contains(e.ProductWarehouseId))
                                            .ToList();

                var previousStock = closingStock
                                        .Where(e => childWarehouseIds.Contains(e.ProductWarehouseId))
                                        .ToList();

                if (isCurrentYear)
                {
                    totalStock += productTransactions.Select(e => e.Quantity).ToList().Sum();
                }

                if (previousStock != null)
                    totalStock += previousStock.Select(e => e.Quantity).Sum();

                entities.Add(new ProductWarehouseSummaryStockOnHandTreeDto
                {
                    IsParent = true,
                    ProductWarehouseId = warehouse.Id,
                    ProductWarehouseName = warehouse.Name,
                    CurrentStock = totalStock
                });
            }
            return entities;
        }
        else if (query.GroupKeys.Count == 1)
        {
            List<ProductWarehouseSummaryStockOnHandTreeDto> entities = [];

            int.TryParse(query.GroupKeys[0], out var parentWarehouseId);

            var warehouses = await _context.ProductWarehouses
                                           .AsNoTracking()
                                           .Where(e => e.ParentId == parentWarehouseId)
                                           .ProjectTo<ProductWarehouseDto>(_mapper.ConfigurationProvider)
                                           .ToListAsync(cancellationToken);

            var warehouseIds = warehouses.Select(e => e.Id).ToList();

            var closingStock = await _context.ProductWarehouseSummaries
                                             .AsNoTracking()
                                             .Where(e => e.ProductPeriodId == previousPeriod.ProductPeriodId
                                                && warehouseIds.Contains(e.ProductWarehouseId))
                                             .ProjectTo<ProductWarehouseSummaryDto>(_mapper.ConfigurationProvider)
                                             .ToListAsync(cancellationToken);

            var transactions = await _context.ProductTransactions
                                             .AsNoTracking()
                                             .Where(e => e.ProductPeriodId == currentPeriod.ProductPeriodId
                                                && warehouseIds.Contains(e.ProductWarehouseId))
                                             .ToListAsync(cancellationToken);

            foreach (var warehouse in warehouses)
            {
                float totalStock = 0;

                var productTransactions = transactions
                                            .Where(e => e.ProductWarehouseId == warehouse.Id)
                                            .ToList();

                var previousStock = closingStock
                                        .Where(e => e.ProductWarehouseId == warehouse.Id)
                                        .ToList();

                if (isCurrentYear)
                {
                    totalStock += productTransactions.Select(e => e.Quantity).ToList().Sum();
                }

                if (previousStock != null)
                    totalStock += previousStock.Select(e => e.Quantity).Sum();

                entities.Add(new ProductWarehouseSummaryStockOnHandTreeDto
                {
                    IsParent = true,
                    ProductWarehouseId = warehouse.Id,
                    ProductWarehouseName = warehouse.Name,
                    CurrentStock = totalStock
                });
            }
            return entities;
        }
        else if (query.GroupKeys.Count == 2)
        {
            List<ProductWarehouseSummaryStockOnHandTreeDto> entities = new();

            int warehouseId;
            try
            {
                warehouseId = int.Parse(query.GroupKeys[1]);
            }
            catch (Exception ex)
            {
                throw new Exception("failed to parse groupKey: " + ex.Message);
            }

            var props = ProductWarehouseSummaryPaginationProps.Create();

            var queryable = _context.ProductWarehouseSummaries
                                    .AsQueryable()
                                    .AsNoTracking()
                                    .Where(e => e.ProductWarehouseId == warehouseId
                                        && e.ProductPeriodId == previousPeriod.ProductPeriodId)
                                    .OrderBy(e => e.ProductId);

            var stockOnHand = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<ProductWarehouseSummaryDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

            var productIds = stockOnHand.DistinctBy(e => e.ProductId).Select(e => e.ProductId).ToList();

            var pastStockAmounts = await _context.ProductWarehouseSummaries
                                           .AsNoTracking()
                                           .Where(e => e.ProductWarehouseId == warehouseId
                                                && productIds.Contains(e.ProductId)
                                                && periodIds.Contains(e.ProductPeriodId)
                                                && e.ProductPeriodId != previousPeriod.ProductPeriodId)
                                           .ProjectTo<ProductWarehouseSummaryDto>(_mapper.ConfigurationProvider)
                                           .ToListAsync(cancellationToken);

            stockOnHand.AddRangeIfNotContains(pastStockAmounts.ToArray());

            List<ProductTransactionDto> transactions = new();

            if (isCurrentYear)
            {
                transactions = await _context.ProductTransactions
                                             .AsNoTracking()
                                             .Where(e => e.ProductWarehouseId == warehouseId
                                                && e.ProductPeriodId == currentPeriod.ProductPeriodId)
                                             .ProjectTo<ProductTransactionDto>(_mapper.ConfigurationProvider)
                                             .ToListAsync(cancellationToken);
            }

            var groupedStockOnHand = stockOnHand.DistinctBy(e => e.ProductId).Select(e => e.ProductId).ToList();

            if (isCurrentYear)
            {
                groupedStockOnHand.AddRange(transactions.DistinctBy(e => e.ProductId).Select(e => e.ProductId).Where(e => !groupedStockOnHand.Contains(e)).ToList());
            }

            foreach (var currentProductId in groupedStockOnHand)
            {
                var currentTransactionsQuantity = transactions
                                            .Where(e => e.ProductId == currentProductId)
                                            .Select(e => e.Quantity)
                                            .ToList()
                                            .Sum();

                var previousClosingStock = stockOnHand
                                            .Where(e => e.ProductId == currentProductId
                                            && e.ProductPeriodId == previousPeriod.ProductPeriodId)
                                            .ToList();
                if (previousClosingStock != null)
                    currentTransactionsQuantity += previousClosingStock.Select(e => e.Quantity).Sum();

                var currentProductStock = stockOnHand
                                            .Where(e => e.ProductId == currentProductId)
                                            .OrderBy(e => e.ProductPeriodId)
                                            .ToList();

                var productName = currentProductStock.Any()
                    ? currentProductStock[0].ProductName
                    : transactions.Where(e => e.ProductId == currentProductId).Select(e => e.ProductName).FirstOrDefault();

                var stockHistory = new ProductWarehouseSummaryStockOnHandTreeDto
                {
                    IsParent = false,
                    ProductId = currentProductId,
                    ProductName = productName,
                    CurrentStock = currentTransactionsQuantity,
                    ProductWarehouseId = warehouseId
                };
                Dictionary<int, float> quantities = new();
                foreach (var period in periods)
                {
                    var periodClose = currentProductStock.Where(e => e.ProductPeriodId == period.ProductPeriodId).Select(e => e.Quantity).Sum();
                    quantities.Add(period.ProductPeriodId, periodClose);
                }
                stockHistory.Quantities = quantities;
                entities.Add(stockHistory);
            }
            return entities;
        }
        throw new Exception("Invalid group keys");
    }
}
