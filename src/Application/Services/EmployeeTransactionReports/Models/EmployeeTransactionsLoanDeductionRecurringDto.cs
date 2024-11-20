namespace Engage.Application.Services.EmployeeTransactionReports.Models;
public class EmployeeTransactionsLoanDeductionRecurringDto : IMapFrom<EmployeeTransaction>
{
    public string EmployeeNumber { get; set; }                      //A
    public decimal InitialAmount { get; set; }                      //B
    public decimal InstallmentAmount { get; set; }                  //C
    public string IsFringeBenefitLoan { get; set; }                   //D
    public string BaseInstallmentOnAmountOrComponent { get; set; }  //E
    public string StartDate { get; set; }                           //F
    public string EndDate { get; set; }                             //G
    public string Comment { get; set; }                             //H

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransaction, EmployeeTransactionsLoanDeductionRecurringDto>()
                .ForMember(d => d.EmployeeNumber, opt => opt.MapFrom(s => s.Employee.Code))
                .ForMember(d => d.InitialAmount, opt => opt.MapFrom(s => Math.Round(s.EmployeeRecurringTransaction.InitialAmount, 2)))
                .ForMember(d => d.InstallmentAmount, opt => opt.MapFrom(s => Math.Round(s.EmployeeRecurringTransaction.InstallmentAmount, 2)))
                .ForMember(d => d.IsFringeBenefitLoan, opt => opt.MapFrom(s => s.EmployeeRecurringTransaction.IsFringeBenefitLoan ? "YES" : "NO"))
                .ForMember(d => d.BaseInstallmentOnAmountOrComponent, opt => opt.MapFrom(s => s.EmployeeRecurringTransaction.BaseInstallmentOnAmountOrComponent))
                .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Note));
    }
}
