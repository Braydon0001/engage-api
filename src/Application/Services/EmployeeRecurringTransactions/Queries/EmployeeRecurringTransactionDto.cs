// auto-generated
namespace Engage.Application.Services.EmployeeRecurringTransactions.Queries;

public class EmployeeRecurringTransactionDto : IMapFrom<EmployeeRecurringTransaction>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int EmployeeTransactionTypeId { get; set; }
    public string EmployeeTransactionTypeName { get; set; }
    public int EmployeeRecurringTransactionStatusId { get; set; }
    public string EmployeeRecurringTransactionStatusName { get; set; }
    public int PayrollPeriodId { get; set; }
    public string PayrollPeriodName { get; set; }
    public int? CreditorBankAccountId { get; set; }
    public string CreditorBankAccountName { get; set; }
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
        profile.CreateMap<EmployeeRecurringTransaction, EmployeeRecurringTransactionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionId));
    }
}
