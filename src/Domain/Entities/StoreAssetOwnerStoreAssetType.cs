namespace Engage.Domain.Entities;

public class StoreAssetOwnerStoreAssetType : BaseAuditableEntity
{
    public int StoreAssetOwnerStoreAssetTypeId { get; set; }
    public int StoreAssetOwnerId { get; set; }
    public int StoreAssetTypeId { get; set; }

    //Navigation Properties

    public StoreAssetOwner StoreAssetOwner { get; set; }
    public StoreAssetType StoreAssetType { get; set; }
}
