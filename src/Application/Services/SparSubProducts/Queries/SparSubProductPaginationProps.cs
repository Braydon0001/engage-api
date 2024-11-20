namespace Engage.Application.Services.SparSubProducts.Queries;

public static class SparSubProductPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            { "id", new("SparSubProductId") },
            { "sparProductName", new("SparProduct.Product") }
        };
    }
}
