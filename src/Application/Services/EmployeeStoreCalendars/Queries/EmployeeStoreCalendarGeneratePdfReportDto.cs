using Engage.Application.Services.SurveyInstances.Models;

namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarGeneratePdfReportDto : IMapFrom<EmployeeStoreCalendar>
{
    public int Id { get; set; }
    public int? SurveryInstanceId { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public List<string> JobTitles { get; set; }
    public string CalendarDate { get; set; }
    public string SurveyInstanceCompletionDate { get; set; }
    public string Note { get; set; }
    public List<SurveyInstanceWebAllAnswersDto> Answers { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendar, EmployeeStoreCalendarGeneratePdfReportDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarId))
            .ForMember(d => d.SurveryInstanceId, opt => opt.MapFrom(s => s.SurveyInstanceId))
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => $"{s.Employee.FirstName} {s.Employee.LastName}"))
            .ForMember(d => d.CalendarDate, opt => opt.MapFrom(s => s.CalendarDate.ToShortDateString()))
            .ForMember(d => d.SurveyInstanceCompletionDate, opt => opt.MapFrom(s => s.CompletionDate != null ? s.CompletionDate.Value.ToShortDateString() : "-"))
            .ForMember(d => d.Note, opt => opt.MapFrom(s => s.SurveyInstance.Note))
            .ForMember(d => d.Answers, opt => opt.Ignore());
    }

}