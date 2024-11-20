namespace Engage.Domain.Entities;

public class ProjectEngageBrand : BaseAuditableEntity
{
    public int ProjectEngageBrandId { get; set; }
    public int ProjectId { get; set; }
    public int EngageBrandId { get; set; }

    // Navigation Properties

    public Project Project { get; set; }
    public EngageBrand EngageBrand { get; set; }
}