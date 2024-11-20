namespace Engage.Application.Services.SurveyFormAnswers.Queries;

public class SurveyFormAnswerDto : IMapFrom<SurveyFormAnswer>
{
    public int Id { get; init; }
    public string AnswerText { get; init; }
    public List<JsonFile> Files { get; init; }
    public List<JsonSetting> Metadata { get; init; }
    public int SurveyFormSubmissionId { get; init; }
    public int SurveyFormQuestionId { get; init; }
    public int? SurveyFormReasonId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswer, SurveyFormAnswerDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormAnswerId));
    }
}
