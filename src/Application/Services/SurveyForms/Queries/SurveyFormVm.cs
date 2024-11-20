
using Engage.Application.Services.Suppliers.Queries;
using Engage.Application.Services.SurveyFormTypes.Queries;

namespace Engage.Application.Services.SurveyForms.Queries;

public class SurveyFormVm : IMapFrom<SurveyForm>
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
    //Remove line 19 after old ensource is deprecated
    public List<JsonFile> Files { get; init; }
    public List<JsonFile> SurveyFormFiles { get; init; }
    public SurveyFormTypeOption SurveyFormTypeId { get; init; }
    public OptionDto EngageSubgroupId { get; init; }
    public SupplierOption SupplierId { get; init; }
    public OptionDto EngageBrandId { get; init; }
    public bool IsStoreRecurring { get; init; }
    public bool IsEmployeeSurvey { get; init; }
    public bool IgnoreSubgroup { get; init; }
    public List<OptionDto> EngageMasterProductIds { get; set; }
    public bool IsTemplate { get; init; }
    public List<JsonLink> Links { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyForm, SurveyFormVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormId))
               .ForMember(d => d.SurveyFormTypeId, opt => opt.MapFrom(s => s.SurveyFormType))
               .ForMember(d => d.EngageSubgroupId, opt => opt.MapFrom(s => s.EngageSubgroupId.HasValue ? new OptionDto(s.EngageSubgroupId.Value, s.EngageSubGroup.Name) : null))
               .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Supplier))
               .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.EngageBrandId.HasValue ? new OptionDto(s.EngageBrandId.Value, s.EngageBrand.Name) : null))
               .ForMember(d => d.SurveyFormFiles, opt => opt.MapFrom(s => s.Files));
    }
}
