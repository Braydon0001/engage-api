// auto-generated
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarVm : IMapFrom<EmployeeStoreCalendar>
{
    public int Id { get; set; }
    public EmployeeOption EmployeeId { get; set; }
    public StoreOption StoreId { get; set; }
    public DateTime CalendarDate { get; set; }
    public int Order { get; set; }
    public int EmployeeStoreCalendarPeriodId { get; set; }
    public int EmployeeStoreCalendarGroupId { get; set; }
    public int? SurveyInstanceId { get; set; }
    public DateTime? SurveyInstanceCompletionDate { get; set; }
    public List<int> SurveyFormSubmissionIds { get; set; }
    public List<SurveyFormOptions> SurveyFormOptions { get; set; }
    public string Note { get; set; }
    public string EmployeeEmail { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendar, EmployeeStoreCalendarVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarId))
               .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Employee))
               .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.Store))
               .ForMember(d => d.SurveyFormSubmissionIds, opt => opt.MapFrom(s => s.SurveyFormSubmissions.Select(e => e.SurveyFormSubmissionId).ToList()))
               //.ForMember(d => d.SurveyFormOptions, opt => opt.MapFrom(s =>
               //     s.SurveyFormSubmissions.Select(e => new OptionDto { Id = e.SurveyFormSubmissionId, Name = e.SurveyFormSubmission.SurveyForm.Title })))
               .ForMember(d => d.SurveyFormOptions, opt => opt.MapFrom(s =>
                    s.SurveyFormSubmissions))
               .ForMember(d => d.EmployeeEmail, opt => opt.Ignore())
               .ForMember(d => d.SurveyInstanceCompletionDate, opt => opt.MapFrom(s => s.CompletionDate));
    }
}

public class SurveyFormOptions : IMapFrom<EmployeeStoreCalendarSurveyFormSubmission>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsCompleted { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarSurveyFormSubmission, SurveyFormOptions>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormSubmissionId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.SurveyFormSubmission.SurveyForm.Title))
               .ForMember(d => d.IsCompleted, opt => opt.MapFrom(s => s.SurveyFormSubmission.IsComplete));
    }
}