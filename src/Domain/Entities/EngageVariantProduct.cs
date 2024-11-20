namespace Engage.Domain.Entities;

public class EngageVariantProduct : BaseMasterEntity
{
    public EngageVariantProduct()
    {
        DCProducts = new HashSet<DCProduct>();
    }
    public int EngageVariantProductId { get; set; }
    public int EngageMasterProductId { get; set; }
    public int UnitTypeId { get; set; }
    public float Size { get; set; }
    public float PackSize { get; set; }
    public string EANNumber { get; set; }
    public string UnitBarcode { get; set; }
    public string CaseBarcode { get; set; }
    public string ShrinkBarcode { get; set; }
    public bool IsMaster { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties
    public EngageMasterProduct EngageMasterProduct { get; set; }
    public UnitType UnitType { get; set; }
    public ICollection<DCProduct> DCProducts { get; set; }
}
