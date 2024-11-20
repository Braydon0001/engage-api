namespace Engage.Application.Services.SurveyFormQuestionTypes.Queries;

public class SurveyFormQuestionTypeOption : IMapFrom<SurveyFormQuestionType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionType, SurveyFormQuestionTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormQuestionTypeId));
    }
}