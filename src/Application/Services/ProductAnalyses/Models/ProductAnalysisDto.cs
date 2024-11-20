namespace Engage.Application.Services.ProductAnalyses.Models;

public class ProductAnalysisDto : IMapFrom<ProductAnalysis>
{
    public int Id { get; set; }
    public int ProductAnalysisGroupId { get; set; }
    public string ProductAnalysisGroupName { get; set; }
    public int ProductAnalysisDivisionId { get; set; }
    public string ProductAnalysisDivisionName { get; set; }
    public int EngageGroupId { get; set; }
    public string EngageGroupName { get; set; }
    public int EngageSubGroupId { get; set; }
    public string EngageSubGroupName { get; set; }
    public int EngageCategoryId { get; set; }
    public string EngageCategoryName { get; set; }
    public int EngageSubCategoryId { get; set; }
    public string EngageSubCategoryName { get; set; }
    public int DistributionCenterId { get; set; }
    public string DistributionCenterName { get; set; }
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
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductAnalysis, ProductAnalysisDto>()
            .ForMember(d => d.Id, s => s.MapFrom(s => s.ProductAnalysisId));
        ;
    }
}
