namespace Engage.Application.Services.ProjectTaskAssignees.Queries;

public class ProjectTaskAssigneeDto : IMapFrom<ProjectTaskAssignee>
{
    public int Id { get; init; }
    public int ProjectTaskId { get; init; }
    public int ProjectStakeholderId { get; init; }
    public int? ProjectTaskStatusId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskAssignee, ProjectTaskAssigneeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskAssigneeId));
    }
}
