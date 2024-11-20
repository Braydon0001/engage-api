namespace Engage.Application.Services.Suppliers.Queries;

public static class SupplierPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            {"id", new("SupplierId") },
            {"code", new("Code") },
            {"name", new("Name") },
            {"shortName", new("ShortName") },
            {"vatNumber", new("VATNumber") },
        };
    }
}
