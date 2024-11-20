namespace Engage.Domain.Entities;

public class ProjectTaskComment : BaseAuditableEntity
{
    public int ProjectTaskCommentId { get; set; }
    public int ProjectTaskId { get; set; }
    public string Comment { get; set; }
    public int ProjectStatusId { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public ProjectTask ProjectTask { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
}