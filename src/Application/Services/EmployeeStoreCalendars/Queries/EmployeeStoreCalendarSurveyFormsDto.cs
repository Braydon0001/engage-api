namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarSurveyFormsDto : IMapFrom<EmployeeStoreCalendar>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public DateTime CalendarDate { get; set; }
    public List<EmployeeStoreCalendarSurveySubmissions> SurveyForms { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendar, EmployeeStoreCalendarSurveyFormsDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarId))
               //.ForMember(d => d.CalendarDate, opt => opt.MapFrom(s => s.CalendarDate.ToShortDateTimeString()))
               .ForMember(d => d.CalendarDate, opt => opt.MapFrom(s => s.CalendarDate))
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
               .ForMember(d => d.SurveyForms, opt => opt.MapFrom(s => s.SurveyFormSubmissions));
    }
}

public class EmployeeStoreCalendarSurveySubmissions : IMapFrom<EmployeeStoreCalendarSurveyFormSubmission>
{
    public int Id { get; set; }
    public string SurveyName { get; set; }
    public bool IsComplete { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarSurveyFormSubmission, EmployeeStoreCalendarSurveySubmissions>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormSubmission.SurveyFormSubmissionId))
               .ForMember(d => d.SurveyName, opt => opt.MapFrom(s => s.SurveyFormSubmission.SurveyForm.Title))
               .ForMember(d => d.IsComplete, opt => opt.MapFrom(s => s.SurveyFormSubmission.IsComplete));
    }
}