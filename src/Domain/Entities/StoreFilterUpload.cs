namespace Engage.Domain.Entities;

public class StoreFilterUpload : BaseFileUploadEntity
{
    public int StoreFilterUploadId { get; set; }
    public string Filter { get; set; }
    public int? StoreId { get; set; }
    public string StoreName { get; set; }

}
