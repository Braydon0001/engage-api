
namespace Engage.Application.Services.SupplierStores.Queries;

public static class SupplierStorePaginatedProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new() {

            { "id", new ("SupplierStoreId") },
            { "supplierId", new ("SupplierId") },
            { "supplierName", new ("Supplier.Name") },
            { "storeId", new ("StoreId") },
            { "storeName", new ("Store.Name") },
            { "engageSubGroupName", new ("EngageSubGroupId", sortProperty: "EngageSubGroup.Name") },
            { "accountNumber", new ("AccountNumber") },
            { "supplierRegionName", new ("SupplierRegionId", sortProperty: "SupplierRegion.Name") },
            { "supplierSubRegionName", new ("SupplierSubRegionId", sortProperty: "SupplierSubRegion.Name") },
            { "frequencyTypeName", new ("FrequencyTypeId", sortProperty: "FrequencyType.Name") },
            { "frequency", new ("Frequency") },
        };
    }
}
