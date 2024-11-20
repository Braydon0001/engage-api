namespace Engage.Domain.Entities;

public class CreditorStatusHistory : BaseAuditableEntity
{
    public int CreditorStatusHistoryId { get; set; }
    public int CreditorId { get; set; }
    public int CreditorStatusId { get; set; }
    public string Reason { get; set; }

    // Navigation Properties

    public Creditor Creditor { get; set; }
    public CreditorStatus CreditorStatus { get; set; }
}