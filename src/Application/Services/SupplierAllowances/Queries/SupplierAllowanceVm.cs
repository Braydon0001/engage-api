// auto-generated
using Engage.Application.Services.Suppliers.Models;
using Engage.Application.Services.SupplierSalesLeads.Queries;

namespace Engage.Application.Services.SupplierAllowances.Queries;

public class SupplierAllowanceVm : IMapFrom<SupplierAllowance>
{
    public int Id { get; set; }
    public SupplierDto SupplierId { get; set; }
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
    public SupplierSalesLeadOption SupplierSalesLeadId { get; set; }
    public string GlSubCode { get; set; }
    public string GlMainCode { get; set; }
    public List<JsonFile> FileNCircular { get; set; }
    public List<JsonFile> FileEngageContract { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierAllowance, SupplierAllowanceVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierAllowanceId))
               .ForMember(d => d.FileNCircular, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "ncircular")))
               .ForMember(d => d.FileEngageContract, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "engagecontract")))
               .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Supplier))
               .ForMember(d => d.SupplierSalesLeadId, opt => opt.MapFrom(s => s.SalesLead));
    }
}
