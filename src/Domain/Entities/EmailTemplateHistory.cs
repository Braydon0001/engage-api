namespace Engage.Domain.Entities;

public class EmailTemplateHistory : BaseAuditableEntity
{
    public int EmailTemplateHistoryId { get; set; }
    public int EmailTemplateId { get; set; }
    public int UserId { get; set; }

    // Navigation Properties
    public EmailTemplate EmailTemplate { get; set; }
    public User User { get; set; }
}
