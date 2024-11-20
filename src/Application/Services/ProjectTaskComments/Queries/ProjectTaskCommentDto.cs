namespace Engage.Application.Services.ProjectTaskComments.Queries;

public class ProjectTaskCommentDto : IMapFrom<ProjectTaskComment>
{
    public int Id { get; init; }
    public int ProjectTaskId { get; init; }
    public string Comment { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskComment, ProjectTaskCommentDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskCommentId));
    }
}
