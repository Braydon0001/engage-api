namespace Engage.Application.Services.EngageSubCategories.Models;

public class EngageSubCategoryVm : IMapFrom<EngageSubCategory>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public OptionDto EngageCategoryId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageSubCategory, EngageSubCategoryVm>()
               .ForMember(d => d.EngageCategoryId, opts => opts.MapFrom(s => new OptionDto(s.EngageCategoryId, s.EngageCategory.Name)));
    }
}
