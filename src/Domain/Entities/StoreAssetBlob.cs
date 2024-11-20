namespace Engage.Domain.Entities;

public class StoreAssetBlob : BaseAuditableEntity
{
    public int StoreAssetBlobId { get; set; }
    public int StoreAssetId { get; set; }
    public string ImageUrl { get; set; }
    public int? StoreAssetFileTypeId { get; set; }
    public List<JsonFile> Files { get; set; }

    //Navigation Properties
    public StoreAsset StoreAsset { get; set; }
    public StoreAssetFileType StoreAssetFileType { get; set; }

}
