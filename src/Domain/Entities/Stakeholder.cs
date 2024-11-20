namespace Engage.Domain.Entities;

public class Stakeholder : BaseAuditableEntity
{
    public Stakeholder()
    {
        Locations = new HashSet<Location>();
        Contacts = new HashSet<Contact>();
    }

    public int StakeholderId { get; set; }
    public int? VendorId { get; set; }
    public int? SupplierId { get; set; }
    public int? StoreId { get; set; }
    public int? EmployeeId { get; set; }

    // Navigation Properties
    public Vendor Vendor { get; set; }
    public Supplier Supplier { get; set; }
    public Store Store { get; set; }
    public Employee Employee { get; set; }
    public StakeholderTypes StakeholderType { get; set; }
    public ICollection<Location> Locations { get; set; }
    public ICollection<Contact> Contacts { get; set; }
    // public ICollection<Asset> Assets { get; set; }
}
