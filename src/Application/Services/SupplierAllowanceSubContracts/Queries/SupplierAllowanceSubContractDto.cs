namespace Engage.Application.Services.SupplierAllowanceSubContracts.Queries;

public class SupplierAllowanceSubContractDto : IMapFrom<SupplierAllowanceSubContract>
{
    public int Id { get; set; }
    public int SupplierAllowanceContractId { get; set; }
    public string SupplierAllowanceContractName { get; set; }
    public string Category { get; set; }
    public string Name { get; set; }
    public string Vendor { get; set; }
    public string Comment { get; set; }
    public string Note { get; set; }
    public string GlSubCode { get; set; }
    public string GlMainCode { get; set; }
    public string NCircular { get; set; }
    public float WarehouseAllowancePercent { get; set; }
    public string WarehouseAllowanceNote { get; set; }
    public float RedistributionPercent { get; set; }
    public string RedistributionNote { get; set; }
    public float SwellPercent { get; set; }
    public string SwellNote { get; set; }
    public float RebatePercent { get; set; }
    public string RebateNote { get; set; }
    public float SettlementPercent { get; set; }
    public string SettlementNote { get; set; }
    public float EncoreHouseAllowancePercent { get; set; }
    public string EncoreHouseAllowanceNote { get; set; }
    public float EncoreTradeMarketingPercent { get; set; }
    public string EncoreTradeMarketingNote { get; set; }
    public float AdvertisingMarketingAllowancePercent { get; set; }
    public string AdvertisingMarketingAllowanceNote { get; set; }
    public float CatmanPercent { get; set; }
    public string CatmanNote { get; set; }
    public float EngagePercent { get; set; }
    public string EngageNote { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierAllowanceSubContract, SupplierAllowanceSubContractDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierAllowanceSubContractId));
    }
}
