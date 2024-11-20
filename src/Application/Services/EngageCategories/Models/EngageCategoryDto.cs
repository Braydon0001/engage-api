namespace Engage.Application.Services.EngageCategories.Models;

public class EngageCategoryDto : IMapFrom<EngageCategory>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public int EngageSubGroupId { get; set; }
    public string EngageSubGroupName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageCategory, EngageCategoryDto>()
            .ForMember(d => d.EngageSubGroupName, opts => opts.MapFrom(s => s.EngageSubGroup.Name));
    }
}
