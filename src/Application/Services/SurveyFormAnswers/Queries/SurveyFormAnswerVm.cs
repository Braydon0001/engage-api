
using Engage.Application.Services.SurveyFormReasons.Queries;
using Engage.Application.Services.SurveyFormSubmissions.Queries;
using SurveyFormQuestionOption = Engage.Application.Services.SurveyFormQuestions.Queries.SurveyFormQuestionOption;

namespace Engage.Application.Services.SurveyFormAnswers.Queries;

public class SurveyFormAnswerVm : IMapFrom<SurveyFormAnswer>
{
    public int Id { get; init; }
    public string AnswerText { get; init; }
    public List<JsonFile> Files { get; init; }
    public List<JsonSetting> Metadata { get; init; }
    public SurveyFormSubmissionOption SurveyFormSubmissionId { get; init; }
    public SurveyFormQuestionOption SurveyFormQuestionId { get; init; }
    public SurveyFormReasonOption SurveyFormReasonId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswer, SurveyFormAnswerVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormAnswerId))
               .ForMember(d => d.SurveyFormSubmissionId, opt => opt.MapFrom(s => s.SurveyFormSubmission))
               .ForMember(d => d.SurveyFormQuestionId, opt => opt.MapFrom(s => s.SurveyFormQuestion))
               .ForMember(d => d.SurveyFormReasonId, opt => opt.MapFrom(s => s.SurveyFormReason));
    }
}
