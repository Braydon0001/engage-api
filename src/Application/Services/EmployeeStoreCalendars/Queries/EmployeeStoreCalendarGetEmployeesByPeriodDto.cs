namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarGetEmployeesByPeriodDto : IMapFrom<EmployeeStoreCalendar>
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeEmail { get; set; }
    public string EmployeeManagerEmail { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendar, EmployeeStoreCalendarGetEmployeesByPeriodDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName))
            .ForMember(d => d.EmployeeEmail, opt => opt.MapFrom(s => s.Employee.EmailAddress1 ?? s.Employee.EmailAddress2))
            .ForMember(d => d.EmployeeManagerEmail, opt => opt.MapFrom(s => s.Employee.Manager.EmailAddress1 ?? s.Employee.Manager.EmailAddress2));
    }
}
