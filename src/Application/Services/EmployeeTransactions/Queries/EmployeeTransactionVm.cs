// auto-generated
using Engage.Application.Services.CreditorBankAccounts.Queries;
using Engage.Application.Services.EmployeeRecurringTransactionStatuses.Queries;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EmployeeTransactionStatuses.Queries;
using Engage.Application.Services.EmployeeTransactionTypes.Queries;
using Engage.Application.Services.PayrollPeriods.Queries;

namespace Engage.Application.Services.EmployeeTransactions.Queries;

public class EmployeeTransactionVm : IMapFrom<EmployeeTransaction>
{
    public int Id { get; set; }
    public EmployeeOption EmployeeId { get; set; }
    public EmployeeTransactionTypeOption EmployeeTransactionTypeId { get; set; }
    public EmployeeTransactionStatusOption EmployeeTransactionStatusId { get; set; }
    public EmployeeRecurringTransactionStatusOption EmployeeRecurringTransactionStatusId { get; set; }
    public PayrollPeriodOption PayrollPeriodId { get; set; }
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
    public CreditorBankAccountOption CreditorBankAccountId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? InitialAmount { get; set; } = 0;
    public decimal? InstallmentAmount { get; set; } = 0;
    public string BaseInstallmentOnAmountOrComponent { get; set; }
    public string Reference { get; set; }
    public bool IsFringeBenefitLoan { get; set; } = false;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransaction, EmployeeTransactionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeTransactionId))
               .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Employee))
               .ForMember(d => d.EmployeeTransactionTypeId, opt => opt.MapFrom(s => s.EmployeeTransactionType))
               .ForMember(d => d.EmployeeTransactionStatusId, opt => opt.MapFrom(s => s.EmployeeTransactionStatus))
               .ForMember(d => d.EmployeeRecurringTransactionStatusId, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionStatus))
               .ForMember(d => d.PayrollPeriodId, opt => opt.MapFrom(s => s.PayrollPeriod))
               .ForMember(d => d.LeavePayPercentage, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.LeavePayPercentage : 0))
               .ForMember(d => d.CreditorBankAccountId, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.CreditorBankAccount : null))
               .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.StartDate : DateTime.Now))
               .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.EndDate : null))
               .ForMember(d => d.InitialAmount, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.InitialAmount : 0))
               .ForMember(d => d.InstallmentAmount, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.InstallmentAmount : 0))
               .ForMember(d => d.BaseInstallmentOnAmountOrComponent, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.BaseInstallmentOnAmountOrComponent : null))
               .ForMember(d => d.Reference, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.Reference : null))
               .ForMember(d => d.IsFringeBenefitLoan, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId.HasValue ? s.EmployeeRecurringTransaction.IsFringeBenefitLoan : false));
    }
}
