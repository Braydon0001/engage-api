namespace Engage.Application.Services.WhatsAppHistories.Queries;

public static class WhatsAppHistoryProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            {"id", new ("WhatsAppHistoryId") },
            {"created", new ("Created.Date") },
            {"toMobileNumber", new ("ToMobileNumber") },
            {"fromMobileNumber", new ("FromMobileNumber") },
            {"fromName", new ("FromName") },
            {"message", new ("Message") },
            {"contentVariables", new ("ContentVariables") },
        };
    }
}
