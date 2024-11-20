namespace Engage.Domain.Entities.LinkEntities;

public class SupplierStore : BaseAuditableEntity
{
    public int SupplierStoreId { get; set; }
    public int SupplierId { get; set; }
    public int StoreId { get; set; }
    public int EngageSubGroupId { get; set; }
    public int FrequencyTypeId { get; set; }
    public int SupplierRegionId { get; set; }
    public int? SupplierSubRegionId { get; set; }
    public int Frequency { get; set; }
    public string Note { get; set; }
    public string AccountNumber { get; set; }

    // Navigation Properties
    public Supplier Supplier { get; set; }
    public Store Store { get; set; }
    public EngageSubGroup EngageSubGroup { get; set; }
    public FrequencyType FrequencyType { get; set; }
    public SupplierRegion SupplierRegion { get; set; }
    public SupplierSubRegion SupplierSubRegion { get; set; }
}
