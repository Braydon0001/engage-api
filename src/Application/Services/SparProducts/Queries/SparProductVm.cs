
using Engage.Application.Services.EvoLedgers.Queries;
using Engage.Application.Services.SparAnalysisGroups.Queries;
using Engage.Application.Services.SparProductStatuses.Queries;
using Engage.Application.Services.SparSystemStatuses.Queries;
using Engage.Application.Services.SparUnitTypes.Queries;
using Engage.Application.Services.Suppliers.Queries;

namespace Engage.Application.Services.SparProducts.Queries;

public class SparProductVm : IMapFrom<SparProduct>
{
    public int Id { get; init; }
    public string ItemCode { get; init; }
    public string Name { get; init; }
    public float? UnitSize { get; init; }
    public SparUnitTypeOption SparUnitTypeId { get; init; }
    public string Barcode { get; init; }
    public OptionDto EngageBrandId { get; init; }
    public SupplierOption SupplierId { get; init; }
    public OptionDto EngageGroupId { get; init; }
    public OptionDto EngageSubGroupId { get; init; }
    public OptionDto EngageCategoryId { get; init; }
    public OptionDto EngageSubCategoryId { get; init; }
    public SparProductStatusOption SparProductStatusId { get; init; }
    public SparAnalysisGroupOption SparAnalysisGroupId { get; init; }
    public SparSystemStatusOption SparSystemStatusId { get; init; }
    public EvoLedgerOption EvoLedgerId { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparProduct, SparProductVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparProductId))
               .ForMember(d => d.SparUnitTypeId, opt => opt.MapFrom(s => s.SparUnitType))
               .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => new OptionDto(s.EngageBrandId, s.EngageBrand.Name)))
               .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Supplier))
               .ForMember(d => d.EngageGroupId, opt => opt.MapFrom(s => new OptionDto(s.EngageSubCategory.EngageCategory.EngageSubGroup.EngageGroupId, s.EngageSubCategory.EngageCategory.EngageSubGroup.EngageGroup.Name)))
               .ForMember(d => d.EngageSubGroupId, opt => opt.MapFrom(s => new OptionDto(s.EngageSubCategory.EngageCategoryId, s.EngageSubCategory.EngageCategory.EngageSubGroup.Name)))
               .ForMember(d => d.EngageCategoryId, opt => opt.MapFrom(s => new OptionDto(s.EngageSubCategory.EngageCategoryId, s.EngageSubCategory.EngageCategory.Name)))
               .ForMember(d => d.EngageSubCategoryId, opt => opt.MapFrom(s => new OptionDto(s.EngageSubCategoryId, s.EngageSubCategory.Name)))
               .ForMember(d => d.SparProductStatusId, opt => opt.MapFrom(s => s.SparProductStatus))
               .ForMember(d => d.SparAnalysisGroupId, opt => opt.MapFrom(s => s.SparAnalysisGroup))
               .ForMember(d => d.SparSystemStatusId, opt => opt.MapFrom(s => s.SparSystemStatus))
               .ForMember(d => d.EvoLedgerId, opt => opt.MapFrom(s => s.EvoLedger));
    }
}
