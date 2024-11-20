namespace Engage.Domain.Entities;

public class CategoryFileEngageRegion : CategoryFileTarget
{
    public int EngageRegionId { get; set; }
    public EngageRegion EngageRegion { get; set; }
}
