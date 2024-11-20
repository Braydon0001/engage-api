namespace Engage.Application.Services.SparProducts.Queries;

public class SparProductDto : IMapFrom<SparProduct>, IMapFrom<SparSubProduct>
{
    public int Id { get; init; }
    public string ItemCode { get; init; }
    public string Name { get; init; }
    public float? UnitSize { get; init; }
    public int? SparUnitTypeId { get; init; }
    public string SparUnitTypeName { get; init; }
    public string Barcode { get; init; }
    public int EngageBrandId { get; init; }
    public string EngageBrandName { get; init; }
    public int SupplierId { get; init; }
    public string SupplierName { get; init; }
    public int EngageSubCategoryId { get; init; }
    public string EngageSubCategoryName { get; init; }
    public int SparProductStatusId { get; init; }
    public string SparProductStatusName { get; init; }
    public int SparSystemStatusId { get; init; }
    public string SparSystemStatusName { get; init; }
    public int EvoLedgerId { get; init; }
    public string EvoLedgerCode { get; init; }
    public bool IsPrimary { get; init; }
    public bool IsParent { get; set; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparProduct, SparProductDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparProductId))
               .ForMember(d => d.EvoLedgerCode, opt => opt.MapFrom(s => s.EvoLedger.LedgerCode));

        profile.CreateMap<SparSubProduct, SparProductDto>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparSubProductId))
        .ForMember(d => d.UnitSize, opt => opt.MapFrom(s => s.SparProduct.UnitSize))
        .ForMember(d => d.ItemCode, opt => opt.MapFrom(s => s.DCCode))
        .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.SparProduct.SupplierId))
        .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.SparProduct.Supplier.Name))
        .ForMember(d => d.SparProductStatusId, opt => opt.MapFrom(s => s.SparSubProductStatusId))
        .ForMember(d => d.SparProductStatusName, opt => opt.MapFrom(s => s.SparSubProductStatus.Name))
        .ForMember(d => d.EngageSubCategoryId, opt => opt.MapFrom(s => s.SparProduct.EngageSubCategoryId))
        .ForMember(d => d.EngageSubCategoryName, opt => opt.MapFrom(s => s.SparProduct.EngageSubCategory.Name))
        .ForMember(d => d.SparSystemStatusId, opt => opt.MapFrom(s => s.SparProduct.SparSystemStatusId))
        .ForMember(d => d.SparSystemStatusName, opt => opt.MapFrom(s => s.SparProduct.SparSystemStatus.Name))
        .ForMember(d => d.EvoLedgerCode, opt => opt.MapFrom(s => s.SparProduct.EvoLedger.LedgerCode));
    }
}
