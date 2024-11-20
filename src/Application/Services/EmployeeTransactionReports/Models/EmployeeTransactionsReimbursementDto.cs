namespace Engage.Application.Services.EmployeeTransactionReports.Models;
public class EmployeeTransactionsReimbursementDto : IMapFrom<EmployeeTransaction>
{
    public string EmployeeNumber { get; set; }          //A
    public decimal Amount { get; set; }                 //B
    public decimal Rate { get; set; }                   //C
    public string StartDate { get; set; }               //D
    public string EndDate { get; set; }                 //E
    public string Comment { get; set; }                 //F

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransaction, EmployeeTransactionsReimbursementDto>()
                .ForMember(d => d.EmployeeNumber, opt => opt.MapFrom(s => s.Employee.Code))
                .ForMember(d => d.Amount, opt => opt.MapFrom(s => Math.Round(s.Amount, 2)))
                .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Note));
    }
}
