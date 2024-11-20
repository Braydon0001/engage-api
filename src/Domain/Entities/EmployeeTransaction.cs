// auto-generated
namespace Engage.Domain.Entities;

public class EmployeeTransaction : BaseAuditableEntity
{
    public int EmployeeTransactionId { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeeTransactionTypeId { get; set; }
    public int? EmployeeRecurringTransactionId { get; set; }
    public int? EmployeeTransactionStatusId { get; set; }
    public int? EmployeeRecurringTransactionStatusId { get; set; }
    public int PayrollPeriodId { get; set; }
    public int EmployeeRecurringTransactionCount { get; set; }
    public DateTime? TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public decimal Rate { get; set; }
    public float Days { get; set; }
    public float Hours { get; set; }
    public float UnpaidDays { get; set; }
    public float UnpaidHours { get; set; }
    public string Note { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public string ApprovedBy { get; set; }
    public DateTime? RejectedDate { get; set; }
    public string RejectedBy { get; set; }
    public string RejectedReason { get; set; }
    public int? EmployeeTransactionRemunerationTypeId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Navigation Properties

    public Employee Employee { get; set; }
    public EmployeeTransactionType EmployeeTransactionType { get; set; }
    public EmployeeRecurringTransaction EmployeeRecurringTransaction { get; set; }
    public EmployeeTransactionStatus EmployeeTransactionStatus { get; set; }
    public EmployeeRecurringTransactionStatus EmployeeRecurringTransactionStatus { get; set; }
    public PayrollPeriod PayrollPeriod { get; set; }
    public EmployeeTransactionRemunerationType EmployeeTransactionRemunerationType { get; set; }
}