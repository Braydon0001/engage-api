namespace Engage.Domain.Entities;

public class PaymentStatusHistory : BaseAuditableEntity
{
    public int PaymentStatusHistoryId { get; set; }
    public int PaymentId { get; set; }
    public int PaymentStatusId { get; set; }
    public string Reason { get; set; }

    // Navigation Properties

    public Payment Payment { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}