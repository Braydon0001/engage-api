namespace Engage.Domain.Entities;

public class ProjectStakeholder : BaseAuditableEntity
{
    public int ProjectStakeholderId { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; private set; }
}