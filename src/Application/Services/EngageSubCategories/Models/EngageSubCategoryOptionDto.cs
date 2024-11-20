namespace Engage.Application.Services.EngageSubCategories.Models;

public class EngageSubCategoryOptionDto : IMapFrom<EngageSubCategory>
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageSubCategory, EngageSubCategoryOptionDto>()
               .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.EngageCategoryId));
    }
}
