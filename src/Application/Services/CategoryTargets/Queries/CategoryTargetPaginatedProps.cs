namespace Engage.Application.Services.CategoryTargets.Queries;

public static class CategoryTargetPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new() {

            { "id", new PaginationProperty("CategoryTargetId") },
            { "storeId", new PaginationProperty("Store.Name") },
            { "supplierId", new PaginationProperty("Supplier.Name") },
            { "target", new PaginationProperty("Target") },
            { "availableLabel", new PaginationProperty("AvailableLabel") },
            { "occupiedLabel", new PaginationProperty("OccupiedLabel") }    
 
       };
    }
}