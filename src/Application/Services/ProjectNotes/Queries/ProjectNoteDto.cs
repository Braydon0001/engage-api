namespace Engage.Application.Services.ProjectNotes.Queries;

public class ProjectNoteDto : IMapFrom<ProjectNote>
{
    public int Id { get; init; }
    public string Note { get; init; }
    public int ProjectId { get; init; }
    public DateTime Created { get; init; }
    public string CreatedBy { get; init; }
    public string UserPhotoUrl { get; set; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectNote, ProjectNoteDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectNoteId));
    }
}
