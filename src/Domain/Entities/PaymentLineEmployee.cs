namespace Engage.Domain.Entities;

public class PaymentLineEmployee : BaseAuditableEntity
{
    public int PaymentLineEmployeeId { get; set; }
    public int PaymentLineId { get; set; }
    public int EmployeeId { get; set; }

    public PaymentLine PaymentLine { get; set; }
    public Employee Employee { get; set; }
}
