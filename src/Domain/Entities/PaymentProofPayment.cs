namespace Engage.Domain.Entities;

public class PaymentProofPayment : BaseAuditableEntity
{
    public int PaymentProofPaymentId { get; set; }
    public int PaymentId { get; set; }
    public int PaymentProofId { get; set; }

    // Navigation Properties

    public Payment Payment { get; set; }
    public PaymentProof PaymentProof { get; set; }
}