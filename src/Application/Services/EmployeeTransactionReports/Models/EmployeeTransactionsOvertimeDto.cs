namespace Engage.Application.Services.EmployeeTransactionReports.Models;
public class EmployeeTransactionsOvertimeDto : IMapFrom<EmployeeTransaction>
{
    public string EmployeeNumber { get; set; }          //A
    public float Hours { get; set; }                   //B
    public string StartDate { get; set; }               //C
    public string EndDate { get; set; }                 //D
    public string Comment { get; set; }                 //E

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransaction, EmployeeTransactionsOvertimeDto>()
                .ForMember(d => d.EmployeeNumber, opt => opt.MapFrom(s => s.Employee.Code))
                .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Note));
    }
}
