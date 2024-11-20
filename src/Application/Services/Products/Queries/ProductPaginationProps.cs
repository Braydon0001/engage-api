namespace Engage.Application.Services.Products.Queries;

public class ProductPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new() {
            {"id", new ("ProductId") },
            {"productName", new ("Product.Name") },
            {"productMasterId", new("Product.ProductMasterId") },
            {"productMasterName", new("Product.ProductMaster.Name") },
            {"productWarehouseId", new("Product.ProductWarehouseId") },
            {"productWarehouseName", new("Product.ProductWarehouse.Name") },
            {"name", new("name") },
            {"code", new("code") },
        };
    }
}
