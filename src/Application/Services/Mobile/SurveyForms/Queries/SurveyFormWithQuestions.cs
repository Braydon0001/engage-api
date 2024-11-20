
using Engage.Application.Services.Suppliers.Queries;
using Engage.Application.Services.SurveyFormQuestionGroups.Queries;
using Engage.Application.Services.SurveyFormTypes.Queries;

namespace Engage.Application.Services.Mobile.SurveyForms.Queries;

public class SurveyFormWithQuestions : IMapFrom<SurveyForm>
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Note { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public bool IsRequired { get; init; }
    public bool IsRecurring { get; init; }
    public bool IsDisabled { get; init; }
    public List<JsonFile> Files { get; init; }
    public List<JsonRule> Rules { get; init; }
    public SurveyFormTypeOption SurveyFormTypeId { get; init; }
    public OptionDto EngageSubgroupId { get; init; }
    public SupplierOption SupplierId { get; init; }
    public OptionDto EngageBrandId { get; init; }
    public bool IsStoreRecurring { get; init; }
    public bool IsEmployeeSurvey { get; init; }
    public bool IgnoreSubgroup { get; init; }
    public List<OptionDto> EngageMasterProductIds { get; set; }
    public List<SurveyFormQuestionGroupVm> SurveyFormQuestionGroups { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyForm, SurveyFormWithQuestions>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormId))
               .ForMember(d => d.SurveyFormTypeId, opt => opt.MapFrom(s => s.SurveyFormType))
               .ForMember(d => d.EngageSubgroupId, opt => opt.MapFrom(s => s.EngageSubgroupId.HasValue ? new OptionDto(s.EngageSubgroupId.Value, s.EngageSubGroup.Name) : null))
               .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Supplier))
               .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.EngageBrandId.HasValue ? new OptionDto(s.EngageBrandId.Value, s.EngageBrand.Name) : null))
               .ForMember(d => d.SurveyFormQuestionGroups, opt => opt.MapFrom(s => s.SurveyFormQuestionGroups))
               .ForMember(d => d.EngageMasterProductIds, opt => opt.MapFrom(s => s.SurveyFormProducts.Select(e => new OptionDto(e.EngageMasterProductId, e.EngageMasterProduct.Name)).ToList()));
    }
}
