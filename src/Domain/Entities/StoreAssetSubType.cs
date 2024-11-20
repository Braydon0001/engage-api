// auto-generated
namespace Engage.Domain.Entities;

public class StoreAssetSubType : BaseAuditableEntity
{
    public StoreAssetSubType()
    {
        StoreAssetTypes = new HashSet<StoreAssetTypeStoreAssetSubType>();
    }
    public int StoreAssetSubTypeId { get; set; }
    public string Name { get; set; }
    public int? StoreAssetTypeId { get; set; }

    // Navigation Properties

    public StoreAssetType StoreAssetType { get; set; }

    public ICollection<StoreAssetTypeStoreAssetSubType> StoreAssetTypes { get; set; }
}