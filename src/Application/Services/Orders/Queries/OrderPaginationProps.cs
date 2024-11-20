namespace Engage.Application.Services.Orders.Queries;

public static class OrderPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            {"id", new PaginationProperty("OrderId") },
            {"orderTypeName", new PaginationProperty("OrderTypeId", sortProperty: "OrderType.Name") },
            {"orderStatusName", new PaginationProperty("OrderStatusId", sortProperty: "OrderStatus.Name") },
            {"orderTemplateName", new PaginationProperty("OrderTemplateId", sortProperty: "OrderTemplate.Name") },
            {"distributionCenterName", new PaginationProperty("DistributionCenterId", sortProperty: "DistributionCenter.Name") },
            {"engageRegionName", new PaginationProperty("Store.EngageRegionId", sortProperty: "Store.EngageRegion.Name") },
            {"supplierName", new PaginationProperty("Supplier.Name") },
            {"storeName", new PaginationProperty("Store.Name") },
            {"orderReference", new PaginationProperty("OrderReference") },
            {"userName", new PaginationProperty("CreatedBy") },
            {"processedBy", new PaginationProperty("ProcessedBy") },
            {"deletedBy", new PaginationProperty("DeletedBy") },
            {"orderDate", new PaginationProperty("OrderDate.Date") },
            {"deliveryDate", new PaginationProperty("DeliveryDate.Value.Date") },
            {"submittedDate", new PaginationProperty("SubmittedDate.Value.Date") },
            {"processedDate", new PaginationProperty("ProcessedDate.Value.Date") },
            {"deletedDate", new PaginationProperty("DeletedDate.Value.Date") },
        };
    }
}
