using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarSurveyVm : IMapFrom<EmployeeStoreCalendar>
{
    public int Id { get; set; }
    public EmployeeOption EmployeeId { get; set; }
    public StoreOption StoreId { get; set; }
    public string CalendarDate { get; set; }
    public int Order { get; set; }
    public int EmployeeStoreCalendarPeriodId { get; set; }
    public int EmployeeStoreCalendarGroupId { get; set; }
    public int SurveyInstanceId { get; set; }
    public bool IsCompleted { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendar, EmployeeStoreCalendarSurveyVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarId))
               .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Employee))
               .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.Store))
               .ForMember(d => d.IsCompleted, opt => opt.MapFrom(s => s.SurveyInstance.IsCompleted));
    }
}
