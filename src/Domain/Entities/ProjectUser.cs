namespace Engage.Domain.Entities;

public class ProjectUser : BaseAuditableEntity
{
    public int ProjectId { get; set; }
    public int UserId { get; set; }

    // Navigation Properties

    public Project Project { get; set; }
    public User User { get; set; }
}