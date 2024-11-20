namespace Engage.Domain.Entities;

public class ProjectTacOpRegion : BaseAuditableEntity
{
    public int ProjectTacOpId { get; set; }
    public int EngageRegionId { get; set; }
    public ProjectTacOp ProjectTacOp { get; set; }
    public EngageRegion EngageRegion { get; set; }
}
