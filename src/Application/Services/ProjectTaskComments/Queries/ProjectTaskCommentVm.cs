
using Engage.Application.Services.ProjectTasks.Queries;

namespace Engage.Application.Services.ProjectTaskComments.Queries;

public class ProjectTaskCommentVm : IMapFrom<ProjectTaskComment>
{
    public int Id { get; init; }
    public ProjectTaskOption ProjectTaskId { get; init; }
    public string Comment { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskComment, ProjectTaskCommentVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskCommentId))
               .ForMember(d => d.ProjectTaskId, opt => opt.MapFrom(s => s.ProjectTask));
    }
}
