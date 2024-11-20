namespace Engage.Application.Services.SurveyFormQuestions.Queries;

public class SurveyFormQuestionOption : IMapFrom<SurveyFormQuestion>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestion, SurveyFormQuestionOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormQuestionId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.QuestionText));
    }
}