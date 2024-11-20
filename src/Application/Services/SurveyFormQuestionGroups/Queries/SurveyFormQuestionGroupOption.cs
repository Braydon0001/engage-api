namespace Engage.Application.Services.SurveyFormQuestionGroups.Queries;

public class SurveyFormQuestionGroupOption : IMapFrom<SurveyFormQuestionGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionGroup, SurveyFormQuestionGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormQuestionGroupId));
    }
}