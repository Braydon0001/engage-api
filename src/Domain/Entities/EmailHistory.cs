namespace Engage.Domain.Entities;

public class EmailHistory : BaseAuditableEntity
{
    public EmailHistory()
    {
        EmailHistoryCCEmails = new HashSet<EmailHistoryCCEmail>();
        EmailHistoryTemplateVariables = new HashSet<EmailHistoryTemplateVariable>();
    }
    public int EmailHistoryId { get; set; }
    public int EmailTemplateId { get; set; }
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Error { get; set; }

    public EmailTemplate EmailTemplate { get; set; }
    public ICollection<EmailHistoryCCEmail> EmailHistoryCCEmails { get; set; }
    public ICollection<EmailHistoryTemplateVariable> EmailHistoryTemplateVariables { get; set; }
}
