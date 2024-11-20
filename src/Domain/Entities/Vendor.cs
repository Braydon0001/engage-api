namespace Engage.Domain.Entities;

public class Vendor : BaseAuditableEntity
{
    public Vendor()
    {
        DCProducts = new HashSet<DCProduct>();
    }
    public int VendorId { get; set; }
    public int DistributionCenterId { get; set; }
    public int SupplierId { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }

    // Navigation Properties
    public DistributionCenter DistributionCenter { get; set; }
    public Supplier Supplier { get; set; }
    public ICollection<DCProduct> DCProducts { get; set; }
}
