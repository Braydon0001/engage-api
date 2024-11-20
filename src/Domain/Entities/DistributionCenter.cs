namespace Engage.Domain.Entities;

public class DistributionCenter : BaseAuditableEntity
{
    public DistributionCenter()
    {
        Warehouses = new HashSet<Warehouse>();
        DCDepts = new HashSet<DCDept>();
        StoreAccounts = new HashSet<DCAccount>();
        DCProducts = new HashSet<DCProduct>();
        Vendors = new HashSet<Vendor>();
    }

    public int DistributionCenterId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    // Navigation Properties        
    public ICollection<Warehouse> Warehouses { get; set; }
    public ICollection<DCProduct> DCProducts { get; set; }
    public ICollection<DCAccount> StoreAccounts { get; set; }
    public ICollection<Vendor> Vendors { get; set; }

    // Many to Many
    public ICollection<DCDept> DCDepts { get; set; }
}
