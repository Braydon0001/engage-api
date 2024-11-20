namespace Engage.Application.Services.OrderStagings.Queries;

public class OrderStagingPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            {"id", new("OrderStagingId") },
            {"region", new("Region") },
            {"storeName", new("StoreName") },
            {"accountNumber", new("AccountNumber") },
            {"orderNumber", new("OrderNumber") },
            {"orderContactName", new("OrderContactName") },
            {"orderContactEmail", new("OrderContactEmail") },
            {"vatNumber", new("VatNumber") }
        };
    }
}
