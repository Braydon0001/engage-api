namespace Engage.Domain.Entities;

public class PaymentLineCostCenter : BaseAuditableEntity
{
    public int PaymentLineCostCenterId { get; set; }
    public int PaymentLineId { get; set; }
    public int CostCenterId { get; set; }

    public PaymentLine PaymentLine { get; set; }
    public CostCenter CostCenter { get; set; }
}
