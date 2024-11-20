namespace Engage.Domain.Entities;

public class StoreAssetFileType : BaseAuditableEntity
{
    public int StoreAssetFileTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
