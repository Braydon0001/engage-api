namespace Engage.Domain.Entities;

public class DCProduct : BaseMasterEntity
{
    public int DCProductId { get; set; }
    public int? EngageVariantProductId { get; set; }
    public int DistributionCenterId { get; set; }
    public int VendorId { get; set; }
    public int? ManufacturerId { get; set; }
    public int ProductClassId { get; set; }
    public int UnitTypeId { get; set; }
    public int ProductActiveStatusId { get; set; }
    public int ProductStatusId { get; set; }
    public int ProductWarehouseStatusId { get; set; }
    public int ProductSubWarehouseId { get; set; }
    public float Size { get; set; }
    public float PackSize { get; set; }
    public string EANNumber { get; set; }
    public string SubWarehouse { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties
    public EngageVariantProduct EngageVariantProduct { get; set; }
    public DistributionCenter DistributionCenter { get; set; }
    public Vendor Vendor { get; set; }
    public Manufacturer Manufacturer { get; set; }
    public DCProductClass ProductClass { get; set; }
    public UnitType UnitType { get; set; }
    public ProductActiveStatus ProductActiveStatus { get; set; }
    public ProductStatus ProductStatus { get; set; }
    public ProductWarehouseStatus ProductWarehouseStatus { get; set; }
    public SubWarehouse ProductSubWarehouse { get; set; }

}
