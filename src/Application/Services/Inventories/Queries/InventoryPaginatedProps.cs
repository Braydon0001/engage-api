namespace Engage.Application.Services.Inventories.Queries;

public static class InventoryPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new() {

            { "id", new PaginationProperty("InventoryId") },
            { "inventoryGroupName", new PaginationProperty("InventoryGroup.Name") },
            { "inventoryStatusName", new PaginationProperty("InventoryStatus.Name") },
            { "inventoryUnitTypeName", new PaginationProperty("InventoryUnitType.Name") },
            { "name", new PaginationProperty("Name") },
            { "description", new PaginationProperty("Description") },
            { "barCode", new PaginationProperty("BarCode") }    
 
       };
    }
}