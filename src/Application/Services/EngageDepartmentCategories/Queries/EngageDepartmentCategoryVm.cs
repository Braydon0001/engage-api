// auto-generated
using Engage.Application.Services.EngageDepartments.Models;

namespace Engage.Application.Services.EngageDepartmentCategories.Queries;

public class EngageDepartmentCategoryVm : IMapFrom<EngageDepartmentCategory>
{
    public int Id { get; set; }
    public EngageDepartmentOption EngageDepartmentId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageDepartmentCategory, EngageDepartmentCategoryVm>()
               .ForMember(d => d.EngageDepartmentId, opt => opt.MapFrom(s => s.EngageDepartment));
    }
}
