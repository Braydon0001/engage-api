namespace Engage.Application.Services.EmployeeTransactionReports.Models;
public class EmployeeTransactionsUnpaidDto : IMapFrom<EmployeeTransaction>
{
    public string EmployeeNumber { get; set; }          //A
    public float UnpaidDays { get; set; }               //B
    public float UnpaidHours { get; set; }              //C
    public string StartDate { get; set; }               //D
    public string EndDate { get; set; }                 //E
    public string Comment { get; set; }                 //F

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransaction, EmployeeTransactionsUnpaidDto>()
                .ForMember(d => d.EmployeeNumber, opt => opt.MapFrom(s => s.Employee.Code))
                .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Note));
    }
}
