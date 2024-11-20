namespace Engage.Application.Services.SurveyFormQuestionGroups.Queries;

public class SurveyFormQuestionGroupOrderDto : IMapFrom<SurveyFormQuestionGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int? DisplayOrder { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionGroup, SurveyFormQuestionGroupOrderDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormQuestionGroupId));
    }
}
