namespace Engage.Application.Services.EngageCategories.Models;

public class EngageCategoryVm : IMapFrom<EngageCategory>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public OptionDto EngageSubGroupId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageCategory, EngageCategoryVm>()
            .ForMember(d => d.EngageSubGroupId, opts => opts.MapFrom(s => new OptionDto(s.EngageSubGroupId, s.EngageSubGroup.Name)));
    }
}
