namespace Engage.Domain.Entities;

public class EngageSubRegion : BaseAuditableEntity
{
    public EngageSubRegion()
    {
        Employees = new HashSet<Employee>();
        Stores = new HashSet<Store>();
    }
    public int EngageSubRegionId { get; set; }
    public int EngageRegionId { get; set; }
    public string Name { get; set; }

    // Navigation Properties
    public EngageRegion EngageRegion { get; set; }
    public ICollection<Employee> Employees { get; set; }
    public ICollection<Store> Stores { get; set; }
}
