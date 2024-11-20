namespace Engage.Application.Services.SurveyFormAnswerHistories.Queries;

public class SurveyFormAnswerHistoryDto : IMapFrom<SurveyFormAnswerHistory>
{
    public int Id { get; init; }
    public string AnswerText { get; init; }
    public List<JsonFile> Files { get; init; }
    public int SurveyFormAnswerId { get; init; }
    public int SurveyFormReasonId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswerHistory, SurveyFormAnswerHistoryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormAnswerHistoryId));
    }
}
