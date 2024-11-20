namespace Engage.Domain.Entities;

public class SparSubProduct : BaseAuditableEntity
{
    public int SparSubProductId { get; set; }
    public int SparProductId { get; set; }
    public string DCCode { get; set; }
    public string Name { get; set; }
    public string Barcode { get; set; }
    public string CaseBarcode { get; set; }
    public string ShrinkBarcode { get; set; }
    public string PalletBarcode { get; set; }
    public bool IsPrimary { get; set; }
    public int SparSubProductStatusId { get; set; }
    public int? SparSourceId { get; set; }
    public int DistributionCenterId { get; set; }
    public string Warehouse { get; set; }
    public float? StockOnHand { get; set; }
    public float? StockOnOrder { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public SparProduct SparProduct { get; set; }
    public SparSubProductStatus SparSubProductStatus { get; set; }
    public SparSource SparSource { get; set; }
    public DistributionCenter DistributionCenter { get; set; }
}