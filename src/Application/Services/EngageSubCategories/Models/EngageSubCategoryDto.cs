namespace Engage.Application.Services.EngageSubCategories.Models;

public class EngageSubCategoryDto : IMapFrom<EngageSubCategory>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public int EngageCategoryId { get; set; }
    public string EngageCategoryName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageSubCategory, EngageSubCategoryDto>()
               .ForMember(d => d.EngageCategoryName, opts => opts.MapFrom(s => s.EngageCategory.Name));
    }
}
