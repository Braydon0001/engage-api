namespace Engage.Application.Services.ProjectAssignees.Queries;

public class ProjectAssigneeOption : IMapFrom<ProjectAssignee>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectAssignee, ProjectAssigneeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectAssigneeId));
    }
}