namespace Engage.Application.Services.ProjectTaskPriorities.Queries;

public class ProjectTaskPriorityOption : IMapFrom<ProjectTaskPriority>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskPriority, ProjectTaskPriorityOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskPriorityId)); ;
    }
}