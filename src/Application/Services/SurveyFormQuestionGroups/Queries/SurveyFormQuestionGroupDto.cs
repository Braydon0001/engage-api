using Engage.Application.Services.SurveyFormQuestions.Queries;

namespace Engage.Application.Services.SurveyFormQuestionGroups.Queries;

public class SurveyFormQuestionGroupDto : IMapFrom<SurveyFormQuestionGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int? DisplayOrder { get; init; }
    public bool IsRequired { get; init; }
    public List<JsonRule> Rules { get; init; }
    public List<JsonFile> Files { get; init; }
    public List<JsonSetting> Metadata { get; init; }
    public int SurveyFormId { get; init; }
    public bool IsVirtualGroup { get; init; }
    public float? CategoryTargetValue { get; init; }
    public bool Disabled { get; init; }
    public List<SurveyFormQuestionDto> Questions { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionGroup, SurveyFormQuestionGroupDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormQuestionGroupId))
               .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.SurveyFormQuestions));
    }
}
