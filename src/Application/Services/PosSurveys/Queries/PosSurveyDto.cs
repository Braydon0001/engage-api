namespace Engage.Application.Services.PosSurveys.Queries;

public class PosSurveyDto : IMapFrom<SurveyFormAnswer>
{
    public int Id { get; set; }
    public string AnswerText { get; set; }
    public DateTime? AnswerDate { get; set; }
    public int SurveyFormQuestionId { get; set; }
    public string SurveyFormQuestionText { get; set; }
    public int SurveyFormSubmissionId { get; init; }
    public string StoreName { get; set; }
    public string StoreCode { get; set; }
    public string DCAccountNumber { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }
    public int SurveyFormId { get; set; }
    public string SurveyName { get; set; }
    public List<JsonFile> Files { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswer, PosSurveyDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormAnswerId))
               .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.SurveyFormSubmission.Store.Name))
               .ForMember(d => d.StoreCode, opt => opt.MapFrom(s => s.SurveyFormSubmission.Store.Code))
               .ForMember(d => d.DCAccountNumber, opt => opt.MapFrom(s => s.SurveyFormSubmission.Store.DCAccounts.Where(e => e.IsPrimary == true).FirstOrDefault().AccountNumber))
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.SurveyFormSubmission.Employee.FirstName + " " + s.SurveyFormSubmission.Employee.LastName))
               .ForMember(d => d.EmployeeCode, opt => opt.MapFrom(s => s.SurveyFormSubmission.Employee.Code))
               .ForMember(d => d.SurveyFormId, opt => opt.MapFrom(s => s.SurveyFormSubmission.SurveyFormId))
               .ForMember(d => d.SurveyName, opt => opt.MapFrom(s => s.SurveyFormSubmission.SurveyForm.Title))
               .ForMember(d => d.AnswerDate, opt => opt.MapFrom(s => s.Created))
               .ForMember(d => d.SurveyFormQuestionText, opt => opt.MapFrom(s => s.SurveyFormQuestion.QuestionText));
    }
}
