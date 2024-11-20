namespace Engage.Application.Services.ProjectTaskAssignees.Queries;

public class ProjectTaskAssigneeOption : IMapFrom<ProjectTaskAssignee>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskAssignee, ProjectTaskAssigneeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskAssigneeId));
    }
}