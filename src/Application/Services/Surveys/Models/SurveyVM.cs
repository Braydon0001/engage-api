namespace Engage.Application.Services.Surveys.Models;

public class SurveyVm : IMapFrom<Survey>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Note { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsRecurring { get; set; }
    public bool IsEmployeeTargeting { get; set; }
    public bool IsRequired { get; set; }
    public bool IsDisabled { get; set; }
    public OptionDto SurveyTypeId { get; set; }
    public OptionDto EngageSubGroupId { get; set; }
    public CascadingOptionDto SupplierId { get; set; }
    public CascadingOptionDto EngageBrandId { get; set; }
    public OptionDto EngageMasterProductId { get; set; }
    public List<OptionDto> EngageRegions { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Survey, SurveyVm>()
            .ForMember(d => d.Id, opts => opts.MapFrom(s => s.SurveyId))
            .ForMember(d => d.IsDisabled, opts => opts.MapFrom(s => s.IsDisabled))
            .ForMember(d => d.SurveyTypeId, opts => opts.MapFrom(s => new OptionDto(s.SurveyTypeId, s.SurveyType.Name)))
            .ForMember(d => d.EngageSubGroupId, opts => opts.MapFrom(s => new OptionDto(s.EngageSubGroupId, s.EngageSubGroup.Name)))
            .ForMember(d => d.SupplierId, opts => opts.MapFrom(s => new CascadingOptionDto(s.SupplierId, s.EngageSubGroupId, s.Supplier.Name)))
            .ForMember(d => d.EngageBrandId, opts => opts.MapFrom(s => new CascadingOptionDto(s.EngageBrandId, s.SupplierId, s.EngageBrand.Name)))
            .ForMember(d => d.EngageMasterProductId, opts => opts.MapFrom(s => s.EngageMasterProductId.HasValue ? new OptionDto(s.EngageMasterProductId.Value, s.EngageMasterProduct.Name) : null))
            .ForMember(d => d.EngageRegions, opts => opts.MapFrom(s => s.SurveyEngageRegions.Select(s => new OptionDto(s.EngageRegionId, s.EngageRegion.Name))));
    }
}
