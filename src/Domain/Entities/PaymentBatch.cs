namespace Engage.Domain.Entities;

public class PaymentBatch : BaseAuditableEntity
{
    public PaymentBatch()
    {
        Payments = new HashSet<Payment>();
        BatchRegions = new HashSet<PaymentBatchRegion>();
    }
    public int PaymentBatchId { get; set; }
    public DateTime BatchDate { get; set; }

    public ICollection<Payment> Payments { get; set; }
    public ICollection<PaymentBatchRegion> BatchRegions { get; set; }
}
