namespace Engage.Domain.Entities;

public class UserRegion : BaseAuditableEntity
{
    public int UserRegionId { get; set; }
    public int UserId { get; set; }
    public int EngageRegionId { get; set; }

    public User User { get; set; }
    public EngageRegion EngageRegion { get; set; }
}
