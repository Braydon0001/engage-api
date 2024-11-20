namespace Engage.Application.Services.EmployeeReports.Models;
public class EmployeeTerminationsDto : IMapFrom<Employee>
{
    public string EmployeeNumber { get; set; }                     //A
    public string EmploymentAction { get; set; }                   //B
    public string GroupJoinDate { get; set; }                    //C
    public string EmploymentDate { get; set; }                   //D
    public string TerminationDate { get; set; }                  //E
    public string TerminationReason { get; set; }                  //F
    public string TerminationRun { get; set; }                     //G
    public string EncashLeave { get; set; }                        //H

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeTerminationsDto>()
                .ForMember(d => d.EmployeeNumber, opt => opt.MapFrom(s => s.Code))
                .ForMember(d => d.EmploymentAction, opt => opt.MapFrom(s => s.EmploymentAction.Name))
                .ForMember(d => d.TerminationReason, opt => opt.MapFrom(s => s.EmployeeTerminationReason.Name))
                .ForMember(d => d.TerminationRun, opt => opt.MapFrom(s => s.PayrollPeriod.PayrollYear.Name + "" + s.PayrollPeriod.EndDate.Month + " (" + s.PayrollPeriod.EndDate.ToString("MMMM") + " - " + s.PayrollPeriod.PayrollYear.Name + ")"))
                .ForMember(d => d.EncashLeave, opt => opt.MapFrom(s => s.IsEncashLeave ? "TRUE" : "FALSE"))
                .ForMember(d => d.EmploymentDate, opt => opt.MapFrom(s => s.StartingDate.ToShortDateString()))
                .ForMember(d => d.GroupJoinDate, opt => opt.MapFrom(s => s.GroupStartDate.ToShortDateString()))
                .ForMember(d => d.TerminationDate, opt => opt.MapFrom(s => s.EndDate.Value.ToShortDateString()));
    }
}
