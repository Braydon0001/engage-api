namespace Engage.Application.Services.EmployeeTransactionReports.Models;
public class EmployeeTransactionsCreditDeductionRecurringDto : IMapFrom<EmployeeTransaction>
{
    public string EmployeeNumber { get; set; }                      //A
    public decimal InitialAmount { get; set; }                      //B
    public decimal InstallmentAmount { get; set; }                  //C
    public string Reference { get; set; }                           //D
    public string CreditorName { get; set; }                        //E
    public string StartDate { get; set; }                           //F
    public string EndDate { get; set; }                             //G
    public string Comment { get; set; }                             //H

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransaction, EmployeeTransactionsCreditDeductionRecurringDto>()
                .ForMember(d => d.EmployeeNumber, opt => opt.MapFrom(s => s.Employee.Code))
                .ForMember(d => d.InitialAmount, opt => opt.MapFrom(s => Math.Round(s.EmployeeRecurringTransaction.InitialAmount, 2)))
                .ForMember(d => d.InstallmentAmount, opt => opt.MapFrom(s => Math.Round(s.EmployeeRecurringTransaction.InstallmentAmount, 2)))
                .ForMember(d => d.Reference, opt => opt.MapFrom(s => s.EmployeeRecurringTransaction.Reference))
                .ForMember(d => d.CreditorName, opt => opt.MapFrom(s => s.EmployeeRecurringTransaction.CreditorBankAccount.Name))
                .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Note));
    }
}
