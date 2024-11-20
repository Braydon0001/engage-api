
using Engage.Application.Services.EngageSubGroups.Models;
using Engage.Application.Services.ProjectCategories.Queries;

namespace Engage.Application.Services.ProjectSubCategories.Queries;

public class ProjectSubCategoryVm : IMapFrom<ProjectSubCategory>
{
    public int Id { get; init; }
    public ProjectCategoryOption ProjectCategoryId { get; init; }
    public EngageSubGroupOption EngageSubGroupId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSubCategory, ProjectSubCategoryVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectSubCategoryId))
               .ForMember(d => d.ProjectCategoryId, opt => opt.MapFrom(s => s.ProjectCategory))
               .ForMember(d => d.EngageSubGroupId, opt => opt.MapFrom(s => s.EngageSubGroup));
    }
}
