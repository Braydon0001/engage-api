namespace Engage.Application.Services.EngageVariantProducts.Queries;

public static class EngageVariantProductPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            { "id", new("EngageVariantProductId") },
            { "name", new("Name") },
            { "code", new("Code") },
            { "unitBarcode", new("UnitBarcode") },
            { "caseBarcode", new("CaseBarcode") },
            { "shrinkBarcode", new("ShrinkBarcode") },
        };
    }
}
