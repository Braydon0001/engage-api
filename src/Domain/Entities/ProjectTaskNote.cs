namespace Engage.Domain.Entities;

public class ProjectTaskNote : BaseAuditableEntity
{
    public int ProjectTaskNoteId { get; set; }
    public string Note { get; set; }
    public int ProjectTaskId { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public ProjectTask ProjectTask { get; set; }
}