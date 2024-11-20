namespace Engage.Domain.Entities;

public class PaymentLineCostSubDepartment : BaseAuditableEntity
{
    public int PaymentLineCostSubDepartmentId { get; set; }
    public int PaymentLineId { get; set; }
    public int CostSubDepartmentId { get; set; }

    public PaymentLine PaymentLine { get; set; }
    public CostSubDepartment CostSubDepartment { get; set; }
}
