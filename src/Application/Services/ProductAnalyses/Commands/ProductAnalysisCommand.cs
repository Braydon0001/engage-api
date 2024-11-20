namespace Engage.Application.Services.ProductAnalyses.Commands;

public class ProductAnalysisCommand
{
    public int ProductAnalysisGroupId { get; set; }
    public int ProductAnalysisDivisionId { get; set; }
    public int EngageGroupId { get; set; }
    public int EngageSubGroupId { get; set; }
    public int EngageCategoryId { get; set; }
    public int EngageSubCategoryId { get; set; }
    public int DistributionCenterId { get; set; }
    public string Supplier { get; set; }
    public string Vendor { get; set; }
    public string Manufacturer { get; set; }
    public string Product { get; set; }
    public string ProductDescription { get; set; }
    public string Size { get; set; }
    public string Key { get; set; }
    public string Barcode { get; set; }
    public string LedgerCode { get; set; }
    public int Listed { get; set; }
    public int New { get; set; }
    public int Sold { get; set; }
    public bool IsButchery { get; set; }
    public bool IsBakery { get; set; }
    public bool IsFresh { get; set; }
    public bool IsHmr { get; set; }
}
