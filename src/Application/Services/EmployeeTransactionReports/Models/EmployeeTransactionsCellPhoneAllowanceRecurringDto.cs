namespace Engage.Application.Services.EmployeeTransactionReports.Models;
public class EmployeeTransactionsCellPhoneAllowanceRecurringDto : IMapFrom<EmployeeTransaction>
{
    public string EmployeeNumber { get; set; }          //A
    public decimal Amount { get; set; }                 //B
    public string StartDate { get; set; }               //C
    public string EndDate { get; set; }                 //D
    public string Comment { get; set; }                 //E

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransaction, EmployeeTransactionsCellPhoneAllowanceRecurringDto>()
                .ForMember(d => d.EmployeeNumber, opt => opt.MapFrom(s => s.Employee.Code))
                .ForMember(d => d.Amount, opt => opt.MapFrom(s => Math.Round(s.Amount, 2)))
                .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Note));
    }
}
