namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class EmployeeStoreCalendarReportVM<T>
{
    public int Count { get; set; }
    public object ReportName { get; set; }
    public List<T> Data { get; set; }
    public List<string> ColumnNames { get; set; }
}

public class EmployeeStoreCalendarPreviousPeriodReportDto : IMapFrom<EmployeeStoreCalendar>
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }
    public string StoreName { get; set; }
    public string StoreCode { get; set; }
    public string CalendarDate { get; set; }
    public string IsCompleted { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendar, EmployeeStoreCalendarPreviousPeriodReportDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarId))
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
            .ForMember(d => d.CalendarDate, opt => opt.MapFrom(s => s.CalendarDate.ToShortDateString()))
            .ForMember(d => d.IsCompleted, opt => opt.MapFrom(s => s.SurveyInstance.IsCompleted));
    }
}

public class EmployeeStoreCalendarCurrentReportDto : IMapFrom<EmployeeStoreCalendar>
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }
    public string StoreName { get; set; }
    public string StoreCode { get; set; }
    public string CalendarDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendar, EmployeeStoreCalendarCurrentReportDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarId))
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
            .ForMember(d => d.CalendarDate, opt => opt.MapFrom(s => s.CalendarDate.ToShortDateString()));
    }
}
