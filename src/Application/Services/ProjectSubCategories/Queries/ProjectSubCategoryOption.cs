namespace Engage.Application.Services.ProjectSubCategories.Queries;

public class ProjectSubCategoryOption : IMapFrom<ProjectSubCategory>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int ParentId { get; init; }
    public string ParentName { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSubCategory, ProjectSubCategoryOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectSubCategoryId))
               .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.ProjectCategoryId))
               .ForMember(d => d.ParentName, opt => opt.MapFrom(s => s.ProjectCategory.Name));
    }
}