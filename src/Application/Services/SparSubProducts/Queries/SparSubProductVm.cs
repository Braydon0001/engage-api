
using Engage.Application.Services.SparProducts.Queries;
using Engage.Application.Services.SparSubProductStatuses.Queries;
using Engage.Application.Services.SparSources.Queries;
using Engage.Application.Services.DistributionCenters.Queries;

namespace Engage.Application.Services.SparSubProducts.Queries;

public class SparSubProductVm : IMapFrom<SparSubProduct>
{
    public int Id { get; init; }
    public SparProductOption SparProductId { get; init; }
    public string DcCode { get; init; }
    public string Name { get; init; }
    public string Barcode { get; init; }
    public string CaseBarcode { get; init; }
    public string ShrinkBarcode { get; init; }
    public string PalletBarcode { get; init; }
    public bool IsPrimary { get; init; }
    public SparSubProductStatusOption SparSubProductStatusId { get; init; }
    public SparSourceOption SparSourceId { get; init; }
    public DistributionCenterOption DistributionCenterId { get; init; }
    public string Warehouse { get; init; }
    public float? StockOnHand { get; init; }
    public float? StockOnOrder { get; init; }
    public string Note { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSubProduct, SparSubProductVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparSubProductId))
               .ForMember(d => d.SparProductId, opt => opt.MapFrom(s => s.SparProduct))
               .ForMember(d => d.SparSubProductStatusId, opt => opt.MapFrom(s => s.SparSubProductStatus))
               .ForMember(d => d.SparSourceId, opt => opt.MapFrom(s => s.SparSource))
               .ForMember(d => d.DistributionCenterId, opt => opt.MapFrom(s => s.DistributionCenter));
    }
}
