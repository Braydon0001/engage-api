namespace Engage.Domain.Entities;

public class DCAccount : BaseAuditableEntity
{
    public int DCAccountId { get; set; }
    public int StoreId { get; set; }
    public int DistributionCenterId { get; set; }
    public string AccountNumber { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPrimary { get; set; }

    // Navigation Properties
    public Store Store { get; set; }
    public DistributionCenter DistributionCenter { get; set; }
}
