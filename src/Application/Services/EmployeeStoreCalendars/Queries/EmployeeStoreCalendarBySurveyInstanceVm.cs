using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarBySurveyInstanceVm : IMapFrom<EmployeeStoreCalendar>
{
    public int Id { get; set; }
    public EmployeeOption EmployeeId { get; set; }
    public StoreOption StoreId { get; set; }
    public string EmployeeEmail { get; set; }
    public string CalendarDate { get; set; }
    public int Order { get; set; }
    public int EmployeeStoreCalendarPeriodId { get; set; }
    public int EmployeeStoreCalendarGroupId { get; set; }
    public int SurveyInstanceId { get; set; }
    public string SurveyInstanceNote { get; set; }
    public List<string> CCEmails { get; set; }
    public List<string> JobTitles { get; set; }
    public string EngageLogo { get; set; }
    public DateTime? SurveyInstanceCompletionDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendar, EmployeeStoreCalendarBySurveyInstanceVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarId))
               .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Employee))
               .ForMember(d => d.EmployeeEmail, opt => opt.MapFrom(s => s.Employee.EmailAddress1))
               .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.Store))
               .ForMember(d => d.SurveyInstanceNote, opt => opt.MapFrom(s => s.SurveyInstance.Note))
               .ForMember(d => d.CCEmails, opt => opt.Ignore());
    }
}
