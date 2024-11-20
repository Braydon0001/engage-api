namespace Engage.Application.Services.EngageCategories.Models;

public class EngageCategoryOptionDto : IMapFrom<EngageCategory>
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageCategory, EngageCategoryOptionDto>()
               .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.EngageSubGroupId));
    }
}
