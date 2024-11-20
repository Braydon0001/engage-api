namespace Engage.Application.Services.EngageSubCategories.Commands;

public class EngageSubCategoryCommand : IMapTo<EngageSubCategory>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int EngageCategoryId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageSubCategoryCommand, EngageSubCategory>();
    }
}
