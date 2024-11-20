namespace Engage.Domain.Entities;

public class PaymentLineDivision : BaseAuditableEntity
{
    public int PaymentLineDivisionId { get; set; }
    public int PaymentLineId { get; set; }
    public int EmployeeDivisionId { get; set; }

    public PaymentLine PaymentLine { get; set; }
    public EmployeeDivision EmployeeDivision { get; set; }
}
