namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarMonthDto : IMapFrom<EmployeeStoreCalendar>
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
    public int EmployeeStoreCalendarTypeId { get; set; }
    public int AnalysisGroupId { get; set; }
    public int SurveyInstanceId { get; set; }
    public bool SurveyCompleted { get; set; }
    public bool IsManagerCreated { get; set; }
    public bool BlockDay { get; set; }
    public string Note { get; set; }
    public bool NewSurvey { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendar, EmployeeStoreCalendarMonthDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarId))
               .ForMember(d => d.CalendarDate, opt => opt.MapFrom(s => s.CalendarDate.ToShortDateTimeString()))
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
               .ForMember(d => d.SurveyCompleted, opt => opt.MapFrom(s =>
                              s.SurveyInstanceId == null
                              ? s.CompletionDate == null ? false
                                : s.SurveyFormSubmissions.Where(e => e.SurveyFormSubmission.IsComplete == false).ToList().Count == 0 ? true
                              : false : s.SurveyInstance.IsCompleted))
               .ForMember(d => d.NewSurvey, opt => opt.MapFrom(s => s.SurveyInstanceId != null ? false : true));
    }
}