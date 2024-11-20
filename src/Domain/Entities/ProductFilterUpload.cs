namespace Engage.Domain.Entities;

public class ProductFilterUpload : BaseFileUploadEntity
{
    public int ProductFilterUploadId { get; set; }
    public string Filter { get; set; }
    public string Barcode { get; set; }
    public int? EngageVariantProductId { get; set; }
    public string EngageVariantProductName { get; set; }
}
