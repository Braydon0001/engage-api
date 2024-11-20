namespace Engage.Domain.Entities;

public class ProjectProjectTagStoreAsset : ProjectProjectTag
{
    public int StoreAssetId { get; set; }
    public StoreAsset StoreAsset { get; set; }
}
