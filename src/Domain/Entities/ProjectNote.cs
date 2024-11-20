namespace Engage.Domain.Entities;

public class ProjectNote : BaseAuditableEntity
{
    public int ProjectNoteId { get; set; }
    public string Note { get; set; }
    public int ProjectId { get; init; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties
    public Project Project { get; set; }
}