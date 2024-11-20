namespace Engage.Application.Services.Stores.Queries;

public static class StorePaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            {"id", new ("StoreId") },
            {"code", new ("Code") },
            {"name", new ("Name") },
            {"vatNumber", new ("VatNumber") },
            {"engageRegionName", new ("EngageRegionId", sortProperty: "EngageRegion.Name") },
            {"storeTypeName", new ("StoreTypeId", sortProperty: "StoreType.Name") },
            {"storeFormatName", new ("StoreFormatId", sortProperty: "StoreFormat.Name") },
            {"storeClusterName", new ("StoreClusterId", sortProperty: "StoreCluster.Name") },
            {"storeLSMName", new ("StoreLSMId", sortProperty: "StoreLSM.Name") },
        };
    }
}
