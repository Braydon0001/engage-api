namespace Engage.Domain.Entities;

public class StoreAssetStoreAssetTypeContact : BaseAuditableEntity
{
    public int StoreAssetStoreAssetTypeContactId { get; set; }
    public int StoreAssetId { get; set; }
    public int StoreAssetTypeContactId { get; set; }

    // Navigation Properties

    public StoreAsset StoreAsset { get; set; }
    public StoreAssetTypeContact StoreAssetTypeContact { get; set; }
}