namespace Engage.Application.Services.SparProducts.Queries;

public static class SparProductPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            { "id", new("SparProductId") },
            { "itemCode", new("ItemCode") },
            { "name", new("Name") },
            { "unitSize", new("UnitSize") },
            { "sparUnitTypeName", new("SparUnitTypeId") },
            { "barcode", new("") }
        };
    }
}
