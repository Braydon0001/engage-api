namespace Engage.Application.Services.PaymentBatches.Queries;

public static class PaymentBatchPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            { "id", new("PaymentBatchId") },
            { "batchDate", new ("BatchDate")},
        };
    }
}
