namespace Engage.Domain.Entities;

public class ProjectTacOp : BaseAuditableEntity
{
    public ProjectTacOp()
    {
        ProjectTacOpRegions = new HashSet<ProjectTacOpRegion>();
    }
    public int ProjectTacOpId { get; set; }
    public int UserId { get; set; }
    public string MobilePhone { get; set; }
    public User User { get; set; }
    public ICollection<ProjectTacOpRegion> ProjectTacOpRegions { get; private set; }
}
