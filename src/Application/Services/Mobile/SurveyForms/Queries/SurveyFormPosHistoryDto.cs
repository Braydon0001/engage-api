namespace Engage.Application.Services.Mobile.SurveyForms.Queries;

public class SurveyFormHistoryDto : IMapFrom<SurveyFormSubmission>
{
    public int SurveyFormSubmissionId { get; set; }
    public int SurveyFormId { get; set; }
    public int? StoreId { get; set; }
    public string SubmissionUuid { get; set; }
    public DateTime Created { get; set; }
    public bool IsComplete { get; set; }
    public DateTime? CompletedDate { get; set; }
    public bool IsAbandoned { get; set; }
    public DateTime? AbandonedDate { get; set; }
    public string Note { get; set; }
    public int? EmployeeId { get; set; }
    public string CreatedBy { get; set; }
    public List<SurveyFormAnswserHistoryDto> Answers { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormSubmission, SurveyFormHistoryDto>();
    }
}

public class SurveyFormAnswserHistoryDto : IMapFrom<SurveyFormAnswer>
{
    public int SurveyFormAnswerId { get; set; }
    public int SurveyFormSubmissionId { get; set; }
    public int SurveyFormQuestionId { get; set; }
    public string Question { get; set; }
    public int SurveyFormQuestionGroupId { get; set; }
    public int? SurveyFormReasonId { get; set; }
    public string AnswerUuid { get; set; }
    public string AnswerText { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<JsonSetting> Metadata { get; set; }
    public DateTime? AnswerDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswer, SurveyFormAnswserHistoryDto>()
            .ForMember(e => e.Question, opt => opt.MapFrom(e => e.SurveyFormQuestion.QuestionText))
            .ForMember(e => e.SurveyFormQuestionGroupId, opt => opt.MapFrom(e => e.SurveyFormQuestion.SurveyFormQuestionGroupId));

    }
}






