namespace Engage.Domain.Entities.LinkEntities;

public class EngageRegionClaimManager
{
    public int EngageRegionId { get; set; }
    public int UserId { get; set; }
    public bool Disabled { get; set; }

    // Navigation Properties
    public EngageRegion EngageRegion { get; set; }
    public User User { get; set; }

}
