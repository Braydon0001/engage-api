namespace Engage.Domain.Entities;

public class Warehouse : BaseAuditableEntity
{
    public int WarehouseId { get; set; }
    public int DCId { get; set; }
    public string Name { get; set; }

    // Navigation Properties
    public DistributionCenter DC { get; set; }
}
