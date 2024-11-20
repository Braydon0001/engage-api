namespace Engage.Domain.Entities;

public class ProductFilter : BaseAuditableEntity
{
    public int ProductFilterId { get; set; }
    public int? EngageVariantProductId { get; set; }
    public string Barcode { get; set; }
    public int? FileUploadId { get; set; }
    public string Filter { get; set; }

    // Navigation Properties
    public EngageVariantProduct EngageVariantProduct { get; set; }
    public FileUpload FileUpload { get; set; }
}
