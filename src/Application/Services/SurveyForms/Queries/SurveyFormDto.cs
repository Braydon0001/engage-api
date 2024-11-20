namespace Engage.Application.Services.SurveyForms.Queries;

public class SurveyFormDto : IMapFrom<SurveyForm>
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
    public int SurveyFormTypeId { get; init; }
    public string SurveyFormTypeName { get; init; }
    public int EngageSubgroupId { get; init; }
    public string EngageSubgroupName { get; init; }
    public int SupplierId { get; init; }
    public string SupplierName { get; init; }
    public int EngageBrandId { get; init; }
    public string EngageBrandName { get; init; }
    public bool IsStoreRecurring { get; init; }
    public bool IsEmployeeSurvey { get; init; }
    public bool IgnoreSubgroup { get; init; }
    public List<JsonRule> Rules { get; init; }
    public bool IsTemplate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyForm, SurveyFormDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormId));
    }
}
