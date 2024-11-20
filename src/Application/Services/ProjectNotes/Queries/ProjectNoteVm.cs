
using Engage.Application.Services.Projects.Queries;

namespace Engage.Application.Services.ProjectNotes.Queries;

public class ProjectNoteVm : IMapFrom<ProjectNote>
{

    //used by mobile
    public int Id { get; init; }
    public string Note { get; init; }
    public ProjectOption ProjectId { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectNote, ProjectNoteVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectNoteId))
               .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => s.Project));
    }
}
