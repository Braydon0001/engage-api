namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarEmployeeReportDto : IMapFrom<EmployeeStoreCalendar>
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }
    public string StoreName { get; set; }
    public string CalendarDate { get; set; }
    public string Status { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendar, EmployeeStoreCalendarEmployeeReportDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarId))
               .ForMember(d => d.CalendarDate, opt => opt.MapFrom(s => s.CalendarDate.ToShortDateString()))
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
               .ForMember(d => d.Status, opt => opt.MapFrom(s => s.CalendarDate.Date <= DateTime.Now.Date
                                                                    ? s.SurveyInstanceId == null
                                                                        ? s.SurveyFormSubmissions.Where(e => e.SurveyFormSubmission.IsComplete == false).ToList().Count == 0
                                                                            ? "Completed"
                                                                            : "Overdue"
                                                                        : s.SurveyInstance.IsCompleted
                                                                            ? "Completed"
                                                                            : "Overdue"
                                                                    : "Future"));
    }
}

public class EmployeeStoreCalendarEmployeeReportData
{
    public EmployeeStoreCalendarEmployeeReportData(List<string> headings, Dictionary<string, List<EmployeeStoreCalendarEmployeeReportDto>> data, string fileName)
    {
        Headings = headings;
        Data = data;
        Count = data.Count;
        FileName = fileName;
    }
    public List<string> Headings { get; set; }
    public Dictionary<string, List<EmployeeStoreCalendarEmployeeReportDto>> Data { get; set; }
    public int Count { get; set; }
    public string FileName { get; set; }
}