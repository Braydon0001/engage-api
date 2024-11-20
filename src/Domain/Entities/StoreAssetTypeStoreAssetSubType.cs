namespace Engage.Domain.Entities;

public class StoreAssetTypeStoreAssetSubType : BaseAuditableEntity
{
    public int StoreAssetTypeStoreAssetSubTypeId { get; set; }
    public int StoreAssetTypeId { get; set; }
    public int StoreAssetSubTypeId { get; set; }

    //Navigation Properties

    public StoreAssetType StoreAssetType { get; set; }
    public StoreAssetSubType StoreAssetSubType { get; set; }
}
