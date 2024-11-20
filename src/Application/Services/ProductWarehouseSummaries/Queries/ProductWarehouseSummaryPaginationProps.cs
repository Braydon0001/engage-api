namespace Engage.Application.Services.ProductWarehouseSummaries.Queries;

public class ProductWarehouseSummaryPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new() {

            { "id", new ("ProductWarehouseSummaryId") },
            { "ag-Grid-AutoColumn", new ("ProductWarehouseId") },
            { "productId", new ("Product.ProductId") },
            { "productName", new ("Product.Name") },
            { "productWarehouseId", new ("ProductWarehouse.ProductWarehouseId") },
            { "productWarehouseName", new ("ProductWarehouse.Name") },
            //{ "productYearId", new ("ProductPeriod.ProductYearId") },
        };
    }
}
