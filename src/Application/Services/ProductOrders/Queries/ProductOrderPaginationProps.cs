namespace Engage.Application.Services.ProductOrders.Queries;

public static class ProductOrderPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            { "id", new("ProductOrderId") },
            { "orderNumber", new("OrderNumber") },
            { "productOrderStatusId", new("ProductOrderStatusId") },
            { "productOrderStatusName", new("ProductOrderStatusId", "ProductOrderStatus.Name") },
            { "productOrderTypeId", new("ProductOrderTypeId") },
            { "productOrderTypeName", new("ProductOrderTypeId", "ProductOrderType.Name") },
            { "productWarehouseName", new("ProductWarehouseId", "productWarehouse.Name") },
            { "productWarehouseId", new("ProductWarehouseId") },
            { "productPeriodName", new("ProductPeriod.Name") },
            { "orderDate", new("OrderDate") },
        };
    }
}
