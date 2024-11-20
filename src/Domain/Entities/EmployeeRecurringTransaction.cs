// auto-generated
namespace Engage.Domain.Entities;

public class EmployeeRecurringTransaction : BaseAuditableEntity
{
    public int EmployeeRecurringTransactionId { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeeTransactionTypeId { get; set; }
    public int EmployeeRecurringTransactionStatusId { get; set; }
    public int PayrollPeriodId { get; set; }
    public int? CreditorBankAccountId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal InitialAmount { get; set; }
    public decimal InstallmentAmount { get; set; }
    public string BaseInstallmentOnAmountOrComponent { get; set; }
    public string Note { get; set; }
    public string Reference { get; set; }
    public bool IsFringeBenefitLoan { get; set; }
    public float LeavePayPercentage { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public string ApprovedBy { get; set; }
    public DateTime? RejectedDate { get; set; }
    public string RejectedBy { get; set; }
    public string RejectedReason { get; set; }

    // Navigation Properties

    public Employee Employee { get; set; }
    public EmployeeTransactionType EmployeeTransactionType { get; set; }
    public EmployeeRecurringTransactionStatus EmployeeRecurringTransactionStatus { get; set; }
    public PayrollPeriod PayrollPeriod { get; set; }
    public CreditorBankAccount CreditorBankAccount { get; set; }
}