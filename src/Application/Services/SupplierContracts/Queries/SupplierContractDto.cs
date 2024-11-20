// auto-generated
namespace Engage.Application.Services.SupplierContracts.Queries;

public class SupplierContractDto : IMapFrom<SupplierContract>
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public int SupplierContractTypeId { get; set; }
    public string SupplierContractTypeName { get; set; }
    public int SupplierContractGroupId { get; set; }
    public string SupplierContractGroupName { get; set; }
    public int SupplierContractSubGroupId { get; set; }
    public string SupplierContractSubGroupName { get; set; }
    public int SupplierContactId { get; set; }
    public string SupplierContactName { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Vendor { get; set; }
    public string Note { get; set; }
    public string Reference1 { get; set; }
    public string Reference2 { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContract, SupplierContractDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractId))
               .ForMember(d => d.SupplierContactName, opt => opt.MapFrom(s => s.SupplierContact.FullName));
    }
}
