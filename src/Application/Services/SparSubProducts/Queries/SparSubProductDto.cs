namespace Engage.Application.Services.SparSubProducts.Queries;

public class SparSubProductDto : IMapFrom<SparSubProduct>
{
    public int Id { get; init; }
    public int SparProductId { get; init; }
    public string SparProductName { get; init; }
    public string DcCode { get; init; }
    public string Name { get; init; }
    public string Barcode { get; init; }
    public string CaseBarcode { get; init; }
    public string ShrinkBarcode { get; init; }
    public string PalletBarcode { get; init; }
    public bool IsPrimary { get; init; }
    public int SparSubProductStatusId { get; init; }
    public string SparSubProductStatusName { get; init; }
    public int? SparSourceId { get; init; }
    public string SparSourceName { get; init; }
    public int DistributionCenterId { get; init; }
    public string DistributionCenterName { get; init; }
    public string Warehouse { get; init; }
    public float? StockOnHand { get; init; }
    public float? StockOnOrder { get; init; }
    public string Note { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSubProduct, SparSubProductDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparSubProductId));
    }
}
