// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarDto : IMapFrom<EmployeeStoreCalendar>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public string CalendarDate { get; set; }
    public int Order { get; set; }
    public int EmployeeStoreCalendarPeriodId { get; set; }
    public string EmployeeStoreCalendarPeriodName { get; set; }
    public int AnalysisGroupId { get; set; }
    public int SurveyInstanceId { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendar, EmployeeStoreCalendarDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarId))
               .ForMember(d => d.CalendarDate, opt => opt.MapFrom(s => s.CalendarDate.ToShortDateTimeString()))
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName));
    }
}
