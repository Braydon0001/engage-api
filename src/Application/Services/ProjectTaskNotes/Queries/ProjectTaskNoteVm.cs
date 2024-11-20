
using Engage.Application.Services.ProjectTasks.Queries;

namespace Engage.Application.Services.ProjectTaskNotes.Queries;

public class ProjectTaskNoteVm : IMapFrom<ProjectTaskNote>
{
    public int Id { get; init; }
    public string Note { get; init; }
    public ProjectTaskOption ProjectTaskIdId { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskNote, ProjectTaskNoteVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskNoteId))
               .ForMember(d => d.ProjectTaskIdId, opt => opt.MapFrom(s => s.ProjectTask));
    }
}
