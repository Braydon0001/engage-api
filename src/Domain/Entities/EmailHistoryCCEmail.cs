namespace Engage.Domain.Entities;

public class EmailHistoryCCEmail : BaseAuditableEntity
{
    public int EmailHistoryCCEmailId { get; set; }
    public int EmailHistoryId { get; set; }
    public string Email { get; set; }
    public EmailHistory EmailHistory { get; set; }
}
