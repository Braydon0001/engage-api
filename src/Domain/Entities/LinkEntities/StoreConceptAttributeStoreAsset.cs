namespace Engage.Domain.Entities.LinkEntities;

public class StoreConceptAttributeStoreAsset
{
    public int StoreConceptAttributeId { get; set; }
    public int StoreAssetId { get; set; }

    //navigation
    public StoreConceptAttribute StoreConceptAttribute { get; set; }
    public StoreAsset StoreAsset { get; set; }
}
