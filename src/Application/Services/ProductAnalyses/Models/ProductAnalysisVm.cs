namespace Engage.Application.Services.ProductAnalyses.Models;

public class ProductAnalysisVm : IMapFrom<ProductAnalysis>
{
    public int Id { get; set; }
    public OptionDto ProductAnalysisGroupId { get; set; }
    public OptionDto ProductAnalysisDivisionId { get; set; }
    public OptionDto EngageGroupId { get; set; }
    public OptionDto EngageSubGroupId { get; set; }
    public OptionDto EngageCategoryId { get; set; }
    public OptionDto EngageSubCategoryId { get; set; }
    public OptionDto DistributionCenterId { get; set; }
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
        profile.CreateMap<ProductAnalysis, ProductAnalysisVm>()
             .ForMember(d => d.Id, s => s.MapFrom(s => s.ProductAnalysisId))
             .ForMember(d => d.ProductAnalysisGroupId, s => s.MapFrom(s => new OptionDto(s.ProductAnalysisGroupId, s.ProductAnalysisGroup.Name)))
             .ForMember(d => d.ProductAnalysisDivisionId, s => s.MapFrom(s => new OptionDto(s.ProductAnalysisDivisionId, s.ProductAnalysisDivision.Name)))
             .ForMember(d => d.EngageGroupId, s => s.MapFrom(s => new OptionDto(s.EngageGroupId, s.EngageGroup.Name)))
             .ForMember(d => d.EngageSubGroupId, s => s.MapFrom(s => new OptionDto(s.EngageSubGroupId, s.EngageSubGroup.Name)))
             .ForMember(d => d.EngageCategoryId, s => s.MapFrom(s => new OptionDto(s.EngageCategoryId, s.EngageCategory.Name)))
             .ForMember(d => d.EngageSubCategoryId, s => s.MapFrom(s => new OptionDto(s.EngageSubCategoryId, s.EngageSubCategory.Name)))
             .ForMember(d => d.DistributionCenterId, s => s.MapFrom(s => new OptionDto(s.DistributionCenterId, s.DistributionCenter.Name)));
    }
}
