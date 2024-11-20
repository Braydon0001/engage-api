namespace Engage.Application.Services.Payments.Queries;

public static class PaymentPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            { "id", new("PaymentId") },
            { "batchDate", new ("PaymentBatch.BatchDate")},
            { "creditorName", new("Creditor.Name") },
            { "invoiceNumber", new("InvoiceNumber") },
            { "paymentStatusName", new ("PaymentStatusId")},
            { "invoiceDate", new ("InvoiceDate")},
            { "createdBy", new ("CreatedBy")},
        };
    }
}
