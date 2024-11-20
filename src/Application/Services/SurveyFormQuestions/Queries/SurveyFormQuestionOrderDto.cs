namespace Engage.Application.Services.SurveyFormQuestions.Queries;

public class SurveyFormQuestionOrderDto : IMapFrom<SurveyFormQuestion>
{
    public int Id { get; init; }
    public string QuestionText { get; init; }
    public int? DisplayOrder { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestion, SurveyFormQuestionOrderDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormQuestionId));
    }
}
