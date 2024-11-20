namespace Engage.Application.Services.Projects.Queries;

public class ProjectOption : IMapFrom<Project>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Project, ProjectOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectId));
    }
}