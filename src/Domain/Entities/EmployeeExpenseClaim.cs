namespace Engage.Domain.Entities;

public class EmployeeExpenseClaim : BaseAuditableEntity
{
    public int EmployeeExpenseClaimId { get; set; }
    public int EmployeeId { get; set; }
    public string Description { get; set; }
    public string RecoverFrom { get; set; }
    public int Value { get; set; }
    public int KMDistanse { get; set; }
    
    public string ManagerComment { get; set; }
    public bool Processed { get; set; }
    public DateTime ClaimDate { get; set; }
    public DateTime SubmittedDate { get; set; }
    public DateTime? ProcessedDate { get; set; }

    public int? StatusId { get; set; }

    // Navigation Properties
    public Employee Employee { get; set; }
    public ExpenseClaimStatus Status { get; set; }
}
