namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarManagerViewDto : IMapFrom<EmployeeStoreCalendar>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }
    public DateTime VisitDate { get; set; }
    public int EmployeeStoreCalendarPeriodId { get; set; }
    public string EmployeeStoreCalendarPeriodName { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendar, EmployeeStoreCalendarManagerViewDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarId))
               .ForMember(d => d.VisitDate, opt => opt.MapFrom(s => s.CalendarDate))
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName));
    }
}
