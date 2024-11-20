namespace Engage.Domain.Entities;

public class ProjectProjectTagEngageRegion : ProjectProjectTag
{
    public int EngageRegionId { get; set; }
    public EngageRegion EngageRegion { get; set; }
}
