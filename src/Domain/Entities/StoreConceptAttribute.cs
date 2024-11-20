namespace Engage.Domain.Entities;

public class StoreConceptAttribute : BaseAuditableEntity
{
    public StoreConceptAttribute()
    {
        StoreConceptAttributeOptions = new HashSet<StoreConceptAttributeOption>();
        StoreConceptAttributeStoreAssets = new HashSet<StoreConceptAttributeStoreAsset>();
    }
    public int StoreConceptAttributeId { get; set; }
    public int StoreConceptId { get; set; }
    public int StoreConceptAttributeTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Navigation Properties 
    public StoreConcept StoreConcept { get; set; }
    public StoreConceptAttributeType StoreConceptAttributeType { get; set; }
    public ICollection<StoreConceptAttributeOption> StoreConceptAttributeOptions { get; set; }

    //many to many
    public ICollection<StoreConceptAttributeStoreAsset> StoreConceptAttributeStoreAssets { get; set; }
}
