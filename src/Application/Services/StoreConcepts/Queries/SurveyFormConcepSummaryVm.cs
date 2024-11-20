namespace Engage.Application.Services.StoreConcepts.Queries;

public class SurveyFormConcepSummaryVm : IMapFrom<SurveyFormSubmission>
{
    public int Id { get; set; }
    public int SurveyFormId { get; set; }
    public string Title { get; set; }
    public DateTime? StartedDate { get; set; }
    public string Note { get; set; }
    public List<SurveyFormConcepSummaryQuestionVm> Answers { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormSubmission, SurveyFormConcepSummaryVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormSubmissionId))
               .ForMember(d => d.Title, opt => opt.MapFrom(s => s.SurveyForm.Title))
               .ForMember(d => d.Answers, opt => opt.MapFrom(s => s.SurveyFormAnswers))
               .ForMember(d => d.StartedDate, src => src.MapFrom(s => s.StartedDate));
    }

}

public class SurveyFormConcepSummaryQuestionVm : IMapFrom<SurveyFormAnswer>, IMapFrom<SurveyFormQuestion>
{
    public int? Id { get; set; }
    public int QuestionId { get; set; }
    public int GroupDisplayOrder { get; set; }
    public int SurveyFormQuestionGroupId { get; set; }
    public int DisplayOrder { get; set; }
    public int SurveyFormQuestionId { get; set; }
    public List<JsonFile> Files { get; set; }
    public string QuestionText { get; set; }
    public string QuestionType { get; set; }
    public string AnswerText { get; set; }
    public List<SurveyFormConcepSummaryQuestionOptionVm> AnswerOptions { get; set; }
    public List<SurveyFormConcepSummaryQuestionOptionVm> QuestionOptions { get; set; }
    public SurveyFormConcepSummaryQuestionOptionVm AnswerReasons { get; set; }
    public List<SurveyFormConcepSummaryQuestionOptionVm> ReasonOptions { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswer, SurveyFormConcepSummaryQuestionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormAnswerId))
               .ForMember(d => d.QuestionId, opt => opt.MapFrom(s => s.SurveyFormQuestionId))
               .ForMember(d => d.GroupDisplayOrder, opt => opt.MapFrom(s => s.SurveyFormQuestion.SurveyFormQuestionGroup.DisplayOrder))
               .ForMember(d => d.DisplayOrder, opt => opt.MapFrom(s => s.SurveyFormQuestion.DisplayOrder))
               .ForMember(d => d.SurveyFormQuestionGroupId, opt => opt.MapFrom(s => s.SurveyFormQuestion.SurveyFormQuestionGroupId))
               .ForMember(d => d.SurveyFormQuestionId, opt => opt.MapFrom(s => s.SurveyFormQuestionId))
               .ForMember(d => d.QuestionText, src => src.MapFrom(s => s.SurveyFormQuestion.QuestionText))
               .ForMember(d => d.AnswerReasons, src => src.MapFrom(s => s.SurveyFormReason))
               .ForMember(d => d.AnswerOptions, src => src.MapFrom(s => s.SurveyFormAnswerOptions))
               .ForMember(d => d.QuestionType, src => src.MapFrom(s => s.SurveyFormQuestion.SurveyFormQuestionType.Name))
               .ForMember(d => d.QuestionOptions, src => src.MapFrom(s => s.SurveyFormQuestion.SurveyFormQuestionOptions))
               .ForMember(d => d.ReasonOptions, src => src.MapFrom(s => s.SurveyFormQuestion.SurveyFormQuestionReasons));

        profile.CreateMap<SurveyFormQuestion, SurveyFormConcepSummaryQuestionVm>()
               .ForMember(d => d.QuestionId, opt => opt.MapFrom(s => s.SurveyFormQuestionId))
               .ForMember(d => d.GroupDisplayOrder, opt => opt.MapFrom(s => s.SurveyFormQuestionGroup.DisplayOrder))
               .ForMember(d => d.DisplayOrder, opt => opt.MapFrom(s => s.DisplayOrder))
               .ForMember(d => d.SurveyFormQuestionGroupId, opt => opt.MapFrom(s => s.SurveyFormQuestionGroupId))
               .ForMember(d => d.SurveyFormQuestionId, opt => opt.MapFrom(s => s.SurveyFormQuestionId))
               .ForMember(d => d.QuestionText, src => src.MapFrom(s => s.QuestionText))
               .ForMember(d => d.AnswerText, src => src.MapFrom(s => ""))
               .ForMember(d => d.QuestionType, src => src.MapFrom(s => s.SurveyFormQuestionType.Name))
               .ForMember(d => d.QuestionOptions, src => src.MapFrom(s => s.SurveyFormQuestionOptions))
               .ForMember(d => d.ReasonOptions, src => src.MapFrom(s => s.SurveyFormQuestionReasons));
    }
}

public class SurveyFormConcepSummaryQuestionOptionVm : IMapFrom<SurveyFormAnswerOption>, IMapFrom<SurveyFormReason>, IMapFrom<SurveyFormQuestionOption>, IMapFrom<SurveyFormQuestionReason>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswerOption, SurveyFormConcepSummaryQuestionOptionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormOptionId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.SurveyFormOption.Name));

        profile.CreateMap<SurveyFormReason, SurveyFormConcepSummaryQuestionOptionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormReasonId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));

        profile.CreateMap<SurveyFormQuestionOption, SurveyFormConcepSummaryQuestionOptionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormOptionId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.SurveyFormOption.Name));

        profile.CreateMap<SurveyFormQuestionReason, SurveyFormConcepSummaryQuestionOptionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormReasonId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.SurveyFormReason.Name));
    }
}