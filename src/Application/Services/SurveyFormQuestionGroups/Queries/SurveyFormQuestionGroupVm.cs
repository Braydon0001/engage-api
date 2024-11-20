using Engage.Application.Services.SurveyFormQuestions.Queries;

namespace Engage.Application.Services.SurveyFormQuestionGroups.Queries;

public class SurveyFormQuestionGroupVm : IMapFrom<SurveyFormQuestionGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int? DisplayOrder { get; init; }
    public bool IsRequired { get; init; }
    public List<JsonRule> Rules { get; init; }
    public List<JsonFile> Files { get; init; }
    public List<JsonFile> SurveyFormQuestionGroupFiles { get; init; }
    public List<JsonSetting> Metadata { get; init; }
    public int SurveyFormId { get; init; }
    public bool IsVirtualGroup { get; init; }
    public float? CategoryTargetValue { get; init; }
    public List<OptionDto> EngageVariantProductIds { get; set; }
    public List<SurveyFormQuestionVm> Questions { get; init; }
    public List<JsonLink> Links { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionGroup, SurveyFormQuestionGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormQuestionGroupId))
               .ForMember(d => d.EngageVariantProductIds, opt => opt.MapFrom(s => new List<OptionDto>(s.SurveyFormQuestionGroupProducts.Select(p => new OptionDto() { Id = p.EngageVariantProductId, Name = p.EngageVariantProduct.Code + " / " + p.EngageVariantProduct.Name }).ToList())))
               .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.SurveyFormQuestions))
               .ForMember(d => d.SurveyFormQuestionGroupFiles, opt => opt.MapFrom(s => s.Files));
    }
}
