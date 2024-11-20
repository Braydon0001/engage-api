namespace Engage.Domain.Entities;

public class StoreAsset : BaseAuditableEntity
{
    public StoreAsset()
    {
        StoreAssetBlobs = new HashSet<StoreAssetBlob>();
        StoreConceptAttributeStoreAssets = new HashSet<StoreConceptAttributeStoreAsset>();
        AssetTypeContacts = new HashSet<StoreAssetStoreAssetTypeContact>();
    }
    public int StoreAssetId { get; set; }
    public int StoreId { get; set; }
    public int? StoreAssetOwnerId { get; set; }
    public int StoreAssetTypeId { get; set; }
    public int? StoreAssetSubTypeId { get; set; }
    public int? StoreAssetConditionId { get; set; }
    public int? StoreAssetStatusId { get; set; }
    public int? AssetStatusId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SerialNumber { get; set; }
    public string EmailAddress { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }
    public DateTime? InstallDate { get; set; }
    public DateTime? UpliftDate { get; set; }
    public bool HasContract { get; set; }

    // Navigation Properties
    public Store Store { get; set; }
    public StoreAssetOwner StoreAssetOwner { get; set; }
    public StoreAssetType StoreAssetType { get; set; }
    public StoreAssetSubType StoreAssetSubType { get; set; }
    public StoreAssetCondition StoreAssetCondition { get; set; }
    public AssetStatus AssetStatus { get; set; }
    public StoreAssetStatus StoreAssetStatus { get; set; }
    public ICollection<StoreAssetBlob> StoreAssetBlobs { get; set; }

    //many to many
    public ICollection<StoreConceptAttributeStoreAsset> StoreConceptAttributeStoreAssets { get; set; }
    public ICollection<StoreAssetStoreAssetTypeContact> AssetTypeContacts { get; set; }
}


