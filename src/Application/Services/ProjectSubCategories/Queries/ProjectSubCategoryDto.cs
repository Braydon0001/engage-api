namespace Engage.Application.Services.ProjectSubCategories.Queries;

public class ProjectSubCategoryDto : IMapFrom<ProjectSubCategory>
{
    public int Id { get; init; }
    public int ProjectCategoryId { get; init; }
    public string ProjectCategoryName { get; init; }
    public string EngageSubGroupName { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSubCategory, ProjectSubCategoryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectSubCategoryId));
    }
}
