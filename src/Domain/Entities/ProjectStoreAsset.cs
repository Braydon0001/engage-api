namespace Engage.Domain.Entities;

public class ProjectStoreAsset : BaseAuditableEntity
{
    public int ProjectStoreAssetId { get; set; }
    public int ProjectId { get; set; }
    public int StoreAssetId { get; set; }

    // Navigation Properties

    public Project Project { get; set; }
    public StoreAsset StoreAsset { get; set; }
}