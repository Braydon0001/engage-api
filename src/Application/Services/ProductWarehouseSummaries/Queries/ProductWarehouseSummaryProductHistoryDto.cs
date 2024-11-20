using Engage.Application.Services.ProductTransactions.Queries;

namespace Engage.Application.Services.ProductWarehouseSummaries.Queries;

public class ProductWarehouseSummaryProductHistoryDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductWarehouseId { get; set; }
    public string ProductWarehouseName { get; set; }
    public float CurrentBalance { get; set; }
    public List<ProductWarehouseSummaryProductHistoryStock> TransactionHistory { get; set; }
}

public class ProductWarehouseSummaryProductHistoryStock
{
    public int Index { get; set; }
    public string ProductPeriodName { get; set; }
    public float StockOpen { get; set; }
    public float? StockClose { get; set; }
    public bool IsCurrentPeriod { get; set; }
    public List<ProductTransactionByProductWarehouseDto> Transactions { get; set; }
}