namespace Engage.Application.Services.ProjectComments.Queries;

public class ProjectCommentDto : IMapFrom<ProjectComment>
{
    public int Id { get; init; }
    public int ProjectId { get; init; }
    public string Comment { get; init; }
    public List<JsonFile> Files { get; init; }
    public string CreatedBy { get; init; }
    public DateTime Created { get; init; }
    public string UserPhotoUrl { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectComment, ProjectCommentDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectCommentId));
    }
}
