namespace Engage.Application.Services.SupplierAllowanceContracts.Queries;

public class SupplierAllowanceContractDto : IMapFrom<SupplierAllowanceContract>
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string SupplierCode { get; set; }
    public int SupplierSalesLeadId { get; set; }
    public string SupplierSalesLeadName { get; set; }
    public string Name { get; set; }
    public string NCircularReference { get; set; }
    public string EncoreReference { get; set; }
    public string Vendor { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Comment { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierAllowanceContract, SupplierAllowanceContractDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierAllowanceContractId))
               .ForMember(d => d.SupplierSalesLeadName, opt => opt.MapFrom(s => $"{s.SupplierSalesLead.FirstName} {s.SupplierSalesLead.LastName}"));
    }
}
