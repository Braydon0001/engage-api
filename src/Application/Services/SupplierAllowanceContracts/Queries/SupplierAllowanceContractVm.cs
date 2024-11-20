using Engage.Application.Services.Suppliers.Models;
using Engage.Application.Services.SupplierSalesLeads.Queries;

namespace Engage.Application.Services.SupplierAllowanceContracts.Queries;

public class SupplierAllowanceContractVm : IMapFrom<SupplierAllowanceContract>
{
    public int Id { get; set; }
    public SupplierDto SupplierId { get; set; }
    public SupplierSalesLeadOption SupplierSalesLeadId { get; set; }
    public string Name { get; set; }
    public string NCircularReference { get; set; }
    public string EncoreReference { get; set; }
    public string Vendor { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Comment { get; set; }
    public string Note { get; set; }
    public List<JsonFile> FileNCircular { get; set; }
    public List<JsonFile> FileEngageContract { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierAllowanceContract, SupplierAllowanceContractVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierAllowanceContractId))
               .ForMember(d => d.FileNCircular, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "ncircular")))
               .ForMember(d => d.FileEngageContract, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "engagecontract")))
               .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Supplier))
               .ForMember(d => d.SupplierSalesLeadId, opt => opt.MapFrom(s => s.SupplierSalesLead));
    }
}
