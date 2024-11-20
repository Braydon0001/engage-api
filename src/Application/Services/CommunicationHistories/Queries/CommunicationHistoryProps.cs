namespace Engage.Application.Services.CommunicationHistories.Queries;

public static class CommunicationHistoryProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            {"id", new ("CommunicationHistoryId") },
            {"created", new ("Created.Date") },
            {"toEmail", new ("ToEmail") },
            {"fromEmail", new ("FromEmail") },
            {"fromName", new ("FromName") },
            {"subject", new ("Subject") },
            {"ccEmails", new ("CcEmails") },
            {"communicationTemplateName", new ("CommunicationTemplateId", sortProperty: "CommunicationTemplate.CommunicationTemplateType.Name") },
        };
    }
}
