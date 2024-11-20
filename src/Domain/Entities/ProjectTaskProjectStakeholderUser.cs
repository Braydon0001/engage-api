namespace Engage.Domain.Entities;

public class ProjectTaskProjectStakeholderUser : BaseAuditableEntity
{
    public int ProjectTaskProjectStakeholderUserId { get; set; }
    public int ProjectTaskId { get; set; }
    public int ProjectStakeholderId { get; set; }

    // Navigation Properties

    public ProjectTask ProjectTask { get; set; }
    public ProjectStakeholderUser ProjectStakeholder { get; set; }
}