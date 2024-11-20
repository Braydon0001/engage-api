namespace Engage.Domain.Entities;

public class EmployeeLoan : BaseAuditableEntity
{
    public int EmployeeLoanId { get; set; }
    public int EmployeeId { get; set; }
    public float Amount { get; set; }
    public float RepayableAmount { get; set; }
    public int LoanTerm { get; set; }
    public DateTime LoanDate { get; set; }
    public float Installment { get; set; }
    public string Reason { get; set; }

    // Navigation Properties
    public Employee Employee { get; set; }
}
