namespace Engage.Application.Services.EngageCategories.Commands;

public class EngageCategoryCommand : IMapTo<EngageCategory>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int EngageSubGroupId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageCategoryCommand, EngageCategory>();
    }
}
