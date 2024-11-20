// auto-generated
namespace Engage.Application.Services.EngageDepartmentCategories.Queries;

public class EngageDepartmentCategoryDto : IMapFrom<EngageDepartmentCategory>
{
    public int Id { get; set; }
    public int EngageDepartmentId { get; set; }
    public string EngageDepartmentName { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageDepartmentCategory, EngageDepartmentCategoryDto>();
    }
}
