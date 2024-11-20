namespace Engage.Domain.Entities;

public class StoreAssetTypeContact : BaseAuditableEntity
{
    public StoreAssetTypeContact()
    {
        StoreAssets = new HashSet<StoreAssetStoreAssetTypeContact>();
        StoreAssetTypes = new HashSet<StoreAssetTypeStoreAssetTypeContact>();
    }
    public int StoreAssetTypeContactId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string MobilePhone { get; set; }

    //Navigation Property

    public ICollection<StoreAssetStoreAssetTypeContact> StoreAssets { get; set; }
    public ICollection<StoreAssetTypeStoreAssetTypeContact> StoreAssetTypes { get; set; }
}