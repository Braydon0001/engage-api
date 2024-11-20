namespace Engage.Domain.Entities;

public class ProjectComment : BaseAuditableEntity
{
    public int ProjectCommentId { get; set; }
    public int ProjectId { get; set; }
    public string Comment { get; set; }
    public int ProjectStatusId { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public Project Project { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
}