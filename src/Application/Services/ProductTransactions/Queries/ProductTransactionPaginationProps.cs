namespace Engage.Application.Services.ProductTransactions.Queries;

public static class ProductTransactionPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new() {

        { "id", new ("ProductTransactionId") },
        { "productName", new ("Product.Name") },
        { "productTransactionTypeName", new ("ProductTransactionTypeId") },
        { "productTransactionStatusName", new ("ProductTransactionStatus.Name") },
        { "productPeriodName", new ("ProductPeriod.Name") },
        { "employeeName", new ("Employee.FirstName") },
        { "productWarehouseName", new ("ProductWarehouseId") },
        { "quantity", new ("Quantity") },
        { "price", new ("Price") },
        { "transactionDate", new ("TransactionDate") },
        //{ "engageRegionNames", new("EngageRegionId")},
        { "engageRegionNames", new("EngageRegion.Name")},
        { "note", new ("Note") }
    };
    }
}
