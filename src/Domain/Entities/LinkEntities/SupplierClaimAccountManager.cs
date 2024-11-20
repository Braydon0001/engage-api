namespace Engage.Domain.Entities.LinkEntities;

public class SupplierClaimAccountManager
{
    public int SupplierId { get; set; }
    public int UserId { get; set; }

    // Navigation Properties
    public Supplier Supplier { get; set; }
    public User User { get; set; }
}
