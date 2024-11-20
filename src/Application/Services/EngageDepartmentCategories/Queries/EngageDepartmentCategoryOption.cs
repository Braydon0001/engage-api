// auto-generated
namespace Engage.Application.Services.EngageDepartmentCategories.Queries;

public class EngageDepartmentCategoryOption : IMapFrom<EngageDepartmentCategory>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageDepartmentCategory, EngageDepartmentCategoryOption>();
    }
}