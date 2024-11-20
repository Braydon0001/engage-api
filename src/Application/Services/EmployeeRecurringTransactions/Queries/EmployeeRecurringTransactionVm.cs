// auto-generated
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EmployeeTransactionTypes.Queries;
using Engage.Application.Services.EmployeeRecurringTransactionStatuses.Queries;
using Engage.Application.Services.PayrollPeriods.Queries;
using Engage.Application.Services.CreditorBankAccounts.Queries;

namespace Engage.Application.Services.EmployeeRecurringTransactions.Queries;

public class EmployeeRecurringTransactionVm : IMapFrom<EmployeeRecurringTransaction>
{
    public int Id { get; set; }
    public EmployeeOption EmployeeId { get; set; }
    public EmployeeTransactionTypeOption EmployeeTransactionTypeId { get; set; }
    public EmployeeRecurringTransactionStatusOption EmployeeRecurringTransactionStatusId { get; set; }
    public PayrollPeriodOption PayrollPeriodId { get; set; }
    public CreditorBankAccountOption CreditorBankAccountId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal InitialAmount { get; set; }
    public decimal InstallmentAmount { get; set; }
    public string BaseInstallmentOnAmountOrComponent { get; set; }
    public string Note { get; set; }
    public string Reference { get; set; }
    public bool IsFringeBenefitLoan { get; set; }
    public float LeavePayPercentage { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeRecurringTransaction, EmployeeRecurringTransactionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId))
               .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Employee))
               .ForMember(d => d.EmployeeTransactionTypeId, opt => opt.MapFrom(s => s.EmployeeTransactionType))
               .ForMember(d => d.EmployeeRecurringTransactionStatusId, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionStatus))
               .ForMember(d => d.PayrollPeriodId, opt => opt.MapFrom(s => s.PayrollPeriod))
               .ForMember(d => d.CreditorBankAccountId, opt => opt.MapFrom(s => s.CreditorBankAccount));
    }
}
