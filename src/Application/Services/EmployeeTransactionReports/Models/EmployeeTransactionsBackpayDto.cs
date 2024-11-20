namespace Engage.Application.Services.EmployeeTransactionReports.Models;
public class EmployeeTransactionsBackpayDto : IMapFrom<EmployeeTransaction>
{
    public string EmployeeNumber { get; set; }          //A
    public decimal Amount { get; set; }                 //B
    public float Days { get; set; }                     //C
    public float Hours { get; set; }                    //D
    public string StartDate { get; set; }               //E
    public string EndDate { get; set; }                 //F
    public string Comment { get; set; }                 //G

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransaction, EmployeeTransactionsBackpayDto>()
                .ForMember(d => d.EmployeeNumber, opt => opt.MapFrom(s => s.Employee.Code))
                .ForMember(d => d.Amount, opt => opt.MapFrom(s => Math.Round(s.Amount, 2)))
                .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Note));
    }
}
