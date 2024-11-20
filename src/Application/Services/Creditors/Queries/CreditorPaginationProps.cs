namespace Engage.Application.Services.Creditors.Queries;

public static class CreditorPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            { "id", new("CreditorId") },
            { "name", new("Name") },
            { "tradingName", new("TradingName") },
            { "creditorStatusId", new ("CreditorStatusId")},
            { "isVatRegistered", new ("IsVatRegistered")},
            { "vatNumber", new ("VatNumber")},
            { "bankConfirmationDate", new ("BankConfirmationDate")},
            { "createdBy", new ("CreatedBy")},
        };
    }
}
