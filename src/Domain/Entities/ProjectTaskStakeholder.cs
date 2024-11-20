namespace Engage.Domain.Entities;

public class ProjectTaskStakeholder : BaseAuditableEntity
{
    public int ProjectTaskStakeholderId { get; set; }
    public int ProjectTaskId { get; set; }
    public int ProjectStakeholderId { get; set; }
    public int ProjectTaskStatusId { get; set; }
    public List<string> Emails { get; set; }
    public ProjectTask ProjectTask { get; private set; }
    public ProjectStakeholder ProjectStakeholder { get; private set; }
    public ProjectTaskStatus ProjectTaskStatus { get; private set; }
}