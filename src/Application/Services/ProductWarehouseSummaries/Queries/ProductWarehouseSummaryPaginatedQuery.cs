using Engage.Application.Services.ProductPeriods.Queries;
using Engage.Application.Services.Products.Queries;
using Engage.Application.Services.ProductTransactions.Queries;

namespace Engage.Application.Services.ProductWarehouseSummaries.Queries;

public record ProductPeriodSum(int ProductId, string ProductName, int ProductPeriodId, int ProductWarehouseId, float Sum);

public record ProductWarehouseQuantitiesDto(int ProductId, string ProductName, int ProductWarehouseId, string ProductWarehouseName, Dictionary<int, float> Quantities, float CurrentStock);

public class ProductWarehouseSummaryPaginatedQuery : PaginatedQuery, IRequest<ListResult<ProductWarehouseQuantitiesDto>>
{
}

public record ProductWarehouseSummaryPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductWarehouseSummaryPaginatedQuery, ListResult<ProductWarehouseQuantitiesDto>>
{
    public async Task<ListResult<ProductWarehouseQuantitiesDto>> Handle(ProductWarehouseSummaryPaginatedQuery query, CancellationToken cancellationToken)
    {
        #region Periods
        var periods = await GetProductPeriods(query, cancellationToken);

        var currentStockPeriod = periods.Where(e => e.StartDate.Date <= DateTime.Now.Date && e.EndDate.Date >= DateTime.Now.Date).FirstOrDefault();

        bool isCurrentYear = currentStockPeriod != null;

        ProductPeriodDto currentPeriod = null;

        if (currentStockPeriod == null)
        {
            currentStockPeriod = periods.OrderBy(e => e.EndDate).Last();
        }
        else
        {
            var pastDate = currentStockPeriod.StartDate.AddDays(-2);

            //currentPeriod = currentStockPeriod;

            currentStockPeriod = await Context.ProductPeriods.AsNoTracking()
                                                       .Where(e => e.StartDate <= pastDate
                                                            && e.EndDate >= pastDate)
                                                       .ProjectTo<ProductPeriodDto>(Mapper.ConfigurationProvider)
                                                       .FirstOrDefaultAsync(cancellationToken)
                                                        ?? throw new Exception("current stock period not found");

            if (!periods.Select(e => e.Id).ToList().Contains(currentStockPeriod.Id))
            {
                periods.Add(currentStockPeriod);
            }
        }

        var periodIds = periods.Select(e => e.Id).ToList();

        #endregion

        if (query.GroupKeys.IsNullOrEmpty())
        {
            var productIds = await GetProductIds(query, currentStockPeriod.Id, cancellationToken);

            // Get the sum of the quantity grouped by the product and the period
            // Generated with AI: https://zzzcode.ai/efcore/chat 
            //var productPeriodSums = from a in Context.ProductWarehouseSummaries
            //                        join b in Context.Products on a.ProductId equals b.ProductId into ab
            //                        from b in ab.DefaultIfEmpty()
            //                        where productIds.Contains(a.ProductId) && periodIds.Contains(a.ProductPeriodId)
            //                        group a.Quantity by new { a.ProductId, a.ProductPeriodId, b.Name, b.ProductWarehouseId } into g
            //                        select new ProductPeriodSum(g.Key.ProductId, g.Key.Name, g.Key.ProductPeriodId, g.Key.ProductWarehouseId, g.Sum());

            var props = ProductWarehouseSummaryPaginationProps.Create();

            var productQuery = Context.ProductWarehouseSummaries
                                           .AsNoTracking()
                                           .Include(e => e.Product)
                                           .Where(e => productIds.Contains(e.ProductId) && periodIds.Contains(e.ProductPeriodId))
                                           .AsQueryable();

            var productHistory = await productQuery//.Filter(query, props)
                                              .ToListAsync(cancellationToken);

            var productHistoryIds = productHistory.Select(e => e.ProductId).ToList();

            var products = await Context.Products
                                  .AsNoTracking()
                                  .Where(e => !productHistoryIds.Contains(e.ProductId))
                                  .OrderBy(e => e.ProductId)
                                  .ProjectTo<ProductDto>(Mapper.ConfigurationProvider)
                                  .ToListAsync(cancellationToken);

            List<ProductTransactionDto> currentPeriodTransactions = [];
            if (currentPeriod != null)
            {
                currentPeriodTransactions = await Context.ProductTransactions
                                                         .AsNoTracking()
                                                         .Where(e => e.ProductPeriodId == currentPeriod.Id
                                                            && (products.Select(e => e.Id).ToList().Contains(e.ProductId)
                                                                || productHistoryIds.Contains(e.ProductId)))
                                                         .ProjectTo<ProductTransactionDto>(Mapper.ConfigurationProvider)
                                                         .ToListAsync(cancellationToken);
            }

            var dtos = new List<ProductWarehouseQuantitiesDto>();
            foreach (var p in productIds)
            {
                var product = products.FirstOrDefault(e => e.Id == p);
                Product productStock = null;
                if (product == null)
                {
                    productStock = productHistory.Where(e => e.ProductId == p).Select(e => e.Product).FirstOrDefault();
                }

                var quantities = new Dictionary<int, float>();
                foreach (var periodId in periodIds)
                {
                    var firstSum = productHistory.Where(e => e.ProductId == p
                                                        && e.ProductPeriodId == periodId)
                                                 .Select(e => e.Quantity).Sum();

                    quantities.Add(periodId, firstSum);
                }
                var test = productHistory.Where(e => e.ProductId == p && e.ProductPeriodId == currentStockPeriod.Id).ToList();
                var currentStock = test.Select(e => e.Quantity).Sum();

                currentStock += currentPeriodTransactions.Where(e => e.ProductId == p).Select(e => e.Quantity).Sum();

                dtos.Add(new
                    (product != null ? product.Id : productStock.ProductId,
                    product != null ? product.Name : productStock.Name,
                    0,
                    "",
                    quantities,
                    currentStock
                ));
            }

            dtos = dtos.OrderBy(e => e.ProductId).ToList();

            return new(dtos);
        }
        else
        {
            // Parse the product id
            if (!int.TryParse(query.GroupKeys.First(), out int productId))
            {
                throw new Exception("GroupKeys (productId) is invalid");
            }

            // Get the summaries
            var summaries = await GetSummaries(query, cancellationToken);

            var warehouseIds = summaries.DistinctBy(e => e.ProductWarehouseId).Select(e => e.ProductWarehouseId).ToList();

            var paginationProps = ProductTransactionPaginationProps.Create();

            var transactionsQuery = Context.ProductTransactions
                                      .AsQueryable()
                                      .AsNoTracking();

            List<ProductTransactionDto> transactions = new();
            if (currentPeriod != null)
            {
                transactions = await transactionsQuery.Where(e => e.ProductId == productId
                                                            && e.ProductPeriodId == currentPeriod.Id)
                                                     .Filter(query, paginationProps)
                                                     .ProjectTo<ProductTransactionDto>(Mapper.ConfigurationProvider)
                                                     .ToListAsync(cancellationToken);
            }

            List<ProductTransactionDto> productTransactions = new();
            if (currentPeriod != null)
            {
                productTransactions = await Context.ProductTransactions
                                                   .AsNoTracking()
                                                   .Where(e => e.ProductId == productId
                                                        && e.ProductPeriodId == currentPeriod.Id)
                                                   .ProjectTo<ProductTransactionDto>(Mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);
            }

            warehouseIds.AddRangeIfNotContains(transactions.DistinctBy(e => e.ProductWarehouseId).Select(e => e.ProductWarehouseId).ToArray());

            // For each distinct warehouse get a dictionary of period and quantity pairs
            var dtos = new List<ProductWarehouseQuantitiesDto>();
            foreach (var warehouseId in warehouseIds)
            {
                var productName = summaries.Where(e => e.ProductWarehouseId == warehouseId).Select(e => e.ProductName).FirstOrDefault()
                    ?? productTransactions.Where(e => e.ProductWarehouseId == warehouseId).Select(e => e.ProductName).FirstOrDefault();

                var warehouseName = summaries.Where(e => e.ProductWarehouseId == warehouseId).Select(e => e.ProductWarehouseName).FirstOrDefault()
                    ?? productTransactions.Where(e => e.ProductWarehouseId == warehouseId).Select(e => e.ProductWarehouseName).FirstOrDefault();

                var currentProductId = summaries.Where(e => e.ProductWarehouseId == warehouseId).Select(e => e.ProductId).FirstOrDefault();

                if (currentProductId < 1)
                    currentProductId = productTransactions.Where(e => e.ProductWarehouseId == warehouseId).Select(e => e.ProductId).FirstOrDefault();

                var quantities = new Dictionary<int, float>();
                foreach (var periodId in periodIds)
                {
                    var firstSummary = summaries.Where(e => e.ProductWarehouseId == warehouseId && e.ProductPeriodId == periodId)
                                                .Select(e => e.Quantity).ToList().Sum();
                    quantities.Add(periodId, firstSummary);
                }
                var currentStock = summaries.Where(e =>
                            e.ProductWarehouseId == warehouseId
                                && e.ProductPeriodId == currentStockPeriod.Id)
                            .Select(e => e.Quantity).FirstOrDefault()
                            + transactions.Where(e => e.ProductWarehouseId == warehouseId)
                            .Select(e => e.Quantity).ToList().Sum();

                dtos.Add(new(currentProductId, productName, warehouseId, warehouseName, quantities, currentStock));
            }

            return new(dtos);
        }
    }

    private async Task<List<int>> GetProductIds(ProductWarehouseSummaryPaginatedQuery query, int currentStockPeriodId, CancellationToken cancellationToken)
    {

        var props = ProductPaginationProps.Create();

        var queryable = Context.ProductWarehouseSummaries.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.ProductPeriodId == currentStockPeriodId);

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.ProductId);
        }

        var pageSize = query.PageSize == 0 ? 100 : query.PageSize;

        var productIds = await queryable.Filter(query, props)
                                  .Sort(query, props)
                                  .Select(e => e.ProductId)
                                  .ToListAsync(cancellationToken);

        return productIds
                    .Distinct()
                    .ToList()
                    .Skip(query.StartRow)
                    .Take(pageSize)
                    .ToList();
    }

    private async Task<List<ProductPeriodDto>> GetProductPeriods(ProductWarehouseSummaryPaginatedQuery query, CancellationToken cancellationToken)
    {

        if (query.FilterModel is not null && query.FilterModel.TryGetValue("productYearId", out var productYearId))
        {
            if (int.TryParse(productYearId.Filter, out var productYearIdParsed))
            {
                return await Context.ProductPeriods.Where(e => e.ProductYearId == productYearIdParsed)
                                                   .OrderBy(e => e.ProductPeriodId)
                                                   .ProjectTo<ProductPeriodDto>(Mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);
            }
            throw new Exception("ProductYearId is invalid");
        }

        throw new Exception("ProductYearId is missing");

    }

    private async Task<List<ProductWarehouseSummaryDto>> GetSummaries(ProductWarehouseSummaryPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = ProductWarehouseSummaryPaginationProps.Create();

        var queryable = Context.ProductWarehouseSummaries.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.ProductId);
        }

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .SkipQuery(query)
                              .TakeQuery(query)
                              .ProjectTo<ProductWarehouseSummaryDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }

}