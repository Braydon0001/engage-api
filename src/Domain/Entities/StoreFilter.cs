namespace Engage.Domain.Entities;

public class StoreFilter : BaseAuditableEntity
{
    public int StoreFilterId { get; set; }
    public int StoreId { get; set; }
    public int? FileUploadId { get; set; }
    public string Filter { get; set; }
    public string AS400 { get; set; }

    // Navigation Properties
    public Store Store { get; set; }
    public FileUpload FileUpload { get; set; }
}
