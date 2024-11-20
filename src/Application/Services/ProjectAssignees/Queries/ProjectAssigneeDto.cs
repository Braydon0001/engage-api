namespace Engage.Application.Services.ProjectAssignees.Queries;

public class ProjectAssigneeDto : IMapFrom<ProjectAssignee>
{
    public int Id { get; init; }
    public int ProjectId { get; init; }
    public int ProjectStakeholderId { get; init; }
    public int? ProjectStatusId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectAssignee, ProjectAssigneeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectAssigneeId));
    }
}
