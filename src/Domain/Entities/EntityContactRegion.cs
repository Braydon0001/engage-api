namespace Engage.Domain.Entities;

public class EntityContactRegion : BaseAuditableEntity
{
    public int EntityContactRegionId { get; set; }
    public int EntityContactId { get; set; }
    public int EngageRegionId { get; set; }

    // Navigation Properties

    public EntityContact EntityContact { get; set; }
    public EngageRegion EngageRegion { get; set; }
}