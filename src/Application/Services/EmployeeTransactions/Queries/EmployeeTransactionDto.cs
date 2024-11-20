// auto-generated
namespace Engage.Application.Services.EmployeeTransactions.Queries;

public class EmployeeTransactionDto : IMapFrom<EmployeeTransaction>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int EmployeeTransactionTypeId { get; set; }
    public string EmployeeTransactionTypeName { get; set; }
    public float? OvertimeMultiple { get; set; }
    public int EmployeeTransactionStatusId { get; set; }
    public string EmployeeTransactionStatusName { get; set; }
    public int? EmployeeRecurringTransactionId { get; set; }
    public int? EmployeeRecurringTransactionStatusId { get; set; }
    public string EmployeeRecurringTransactionStatusName { get; set; }
    public int? EmployeeTransactionRemunerationTypeId { get; set; }
    public string EmployeeTransactionRemunerationTypeName { get; set; }
    public int PayrollPeriodId { get; set; }
    public string PayrollPeriodName { get; set; }
    public int EmployeeRecurringTransactionCount { get; set; }
    public DateTime? TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public decimal Rate { get; set; }
    public float Days { get; set; }
    public float Hours { get; set; }
    public float UnpaidDays { get; set; }
    public float UnpaidHours { get; set; }
    public string Note { get; set; }
    //recurring
    public float LeavePayPercentage { get; set; }
    public int? CreditorBankAccountId { get; set; }
    public string CreditorBankAccountName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public decimal? InitialAmount { get; set; } = 0;
    public decimal? InstallmentAmount { get; set; } = 0;
    public string BaseInstallmentOnAmountOrComponent { get; set; }
    public string Reference { get; set; }
    public bool IsFringeBenefitLoan { get; set; } = false;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransaction, EmployeeTransactionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeTransactionId))
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => $"{s.Employee.FirstName} {s.Employee.LastName} - {s.Employee.Code}"))
               .ForMember(d => d.LeavePayPercentage, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.LeavePayPercentage : 0))
               .ForMember(d => d.OvertimeMultiple, opt => opt.MapFrom(s => s.EmployeeTransactionType.OvertimeMultiple))
               .ForMember(d => d.CreditorBankAccountId, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.CreditorBankAccountId : null))
               .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.StartDate : s.StartDate.HasValue ? s.StartDate : null))
               .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.EndDate : s.EndDate))
               .ForMember(d => d.InitialAmount, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.InitialAmount : 0))
               .ForMember(d => d.InstallmentAmount, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.InstallmentAmount : 0))
               .ForMember(d => d.BaseInstallmentOnAmountOrComponent, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.BaseInstallmentOnAmountOrComponent : null))
               .ForMember(d => d.Reference, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.Reference : null))
               .ForMember(d => d.IsFringeBenefitLoan, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.IsFringeBenefitLoan : false))
               .ForMember(d => d.CreditorBankAccountName, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.CreditorBankAccountId.HasValue ? s.EmployeeRecurringTransaction.CreditorBankAccount.Name : null : null));
    }
}
