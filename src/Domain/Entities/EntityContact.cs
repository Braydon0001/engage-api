namespace Engage.Domain.Entities;

// See table-per-hierarchy (TPH) 
// https://docs.microsoft.com/en-us/ef/core/modeling/inheritance

public class EntityContact : BaseAuditableEntity
{
    public EntityContact()
    {
        CommunicationTypes = new HashSet<EntityContactCommunicationType>();
        EntityContactRegions = new HashSet<EntityContactRegion>();
    }
    public int EntityContactId { get; set; }
    public int EntityContactTypeId { get; set; }
    public int? UserId { get; set; }
    public string EmailAddress1 { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string MiddleName { get; set; }
    public string MobilePhone { get; set; }
    public string Description { get; set; }
    public List<JsonFile> Files { get; set; }

    //Navigation Properties
    public EntityContactType EntityContactType { get; set; }
    public User User { get; set; }
    public ICollection<EntityContactCommunicationType> CommunicationTypes { get; set; }
    public ICollection<EntityContactRegion> EntityContactRegions { get; set; }
}

public class EngageRegionContact : EntityContact
{
    public int EngageRegionId { get; set; }

    //Navigation Properties
    public EngageRegion EngageRegion { get; set; }
}

public class StoreContact : EntityContact
{
    public int StoreId { get; set; }
    public bool IsSupplier { get; set; }

    //Navigation Properties
    public Store Store { get; set; }
}

public class SupplierContact : EntityContact
{
    public int SupplierId { get; set; }

    //Navigation Properties
    public Supplier Supplier { get; set; }
}
