// auto-generated
namespace Engage.Application.Services.SupplierAllowances.Queries;

public class SupplierAllowanceDto : IMapFrom<SupplierAllowance>
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string SupplierCode { get; set; }
    public string Vendor { get; set; }
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
    public string Comment { get; set; }
    public string Note { get; set; }
    public int SupplierSalesLeadId { get; set; }
    public string SupplierSalesName { get; set; }
    public string SupplierSalesLeadEmailAddress { get; set; }
    public string SupplierSalesLeadContactNumber { get; set; }
    public string GlSubCode { get; set; }
    public string GlMainCode { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierAllowance, SupplierAllowanceDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierAllowanceId))
               .ForMember(d => d.SupplierSalesName, opt => opt.MapFrom(s => $"{s.SalesLead.FirstName} {s.SalesLead.LastName}"));
    }
}
