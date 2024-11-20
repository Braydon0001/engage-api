namespace Engage.Application.Services.ProjectCategories.Queries;

public class ProjectCategoryOption : IMapFrom<ProjectCategory>
{
    public int Id { get; init; }
    public string Name { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCategory, ProjectCategoryOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectCategoryId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
    }
}