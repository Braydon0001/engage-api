
using Engage.Application.Services.Projects.Queries;

namespace Engage.Application.Services.ProjectComments.Queries;

public class ProjectCommentVm : IMapFrom<ProjectComment>
{
    public int Id { get; init; }
    public ProjectOption ProjectId { get; init; }
    public string Comment { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectComment, ProjectCommentVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectCommentId))
               .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => s.Project));
    }
}
