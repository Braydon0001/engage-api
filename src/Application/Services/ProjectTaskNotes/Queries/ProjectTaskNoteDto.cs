namespace Engage.Application.Services.ProjectTaskNotes.Queries;

public class ProjectTaskNoteDto : IMapFrom<ProjectTaskNote>
{
    public int Id { get; init; }
    public string Note { get; init; }
    public int ProjectTaskId { get; init; }
    public string ProjectTaskName { get; init; }
    public DateTime Created { get; init; }
    public string CreatedBy { get; init; }
    public string UserPhotoUrl { get; set; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskNote, ProjectTaskNoteDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskNoteId));
    }
}
