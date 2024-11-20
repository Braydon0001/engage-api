namespace Engage.Domain.Entities;

public class PaymentBatchRegion : BaseAuditableEntity
{
    public int PaymentBatchRegionId { get; set; }
    public int PaymentBatchId { get; set; }
    public int EngageRegionId { get; set; }

    public PaymentBatch PaymentBatch { get; set; }
    public EngageRegion EngageRegion { get; set; }
}
