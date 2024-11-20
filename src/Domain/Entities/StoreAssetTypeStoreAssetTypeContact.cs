namespace Engage.Domain.Entities;

public class StoreAssetTypeStoreAssetTypeContact : BaseAuditableEntity
{
    public int StoreAssetTypeStoreAssetTypeContactId { get; set; }
    public int StoreAssetTypeId { get; set; }
    public int StoreAssetTypeContactId { get; set; }

    // Navigation Properties

    public StoreAssetType StoreAssetType { get; set; }
    public StoreAssetTypeContact StoreAssetTypeContact { get; set; }
}