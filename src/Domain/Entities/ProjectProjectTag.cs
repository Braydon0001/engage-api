namespace Engage.Domain.Entities;

public class ProjectProjectTag : BaseAuditableEntity
{
    public int ProjectProjectTagId { get; set; }
    public int ProjectId { get; set; }

    // Navigation Properties

    public Project Project { get; set; }
}