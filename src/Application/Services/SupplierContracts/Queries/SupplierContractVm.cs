// auto-generated
using Engage.Application.Services.SupplierContractGroups.Queries;
using Engage.Application.Services.SupplierContractSubGroups.Queries;
using Engage.Application.Services.SupplierContractTypes.Queries;

namespace Engage.Application.Services.SupplierContracts.Queries;

public class SupplierContractVm : IMapFrom<SupplierContract>
{
    public int Id { get; set; }
    public OptionDto SupplierId { get; set; }
    public SupplierContractTypeOption SupplierContractTypeId { get; set; }
    public SupplierContractGroupOption SupplierContractGroupId { get; set; }
    public SupplierContractSubGroupOption SupplierContractSubGroupId { get; set; }
    public OptionDto SupplierContactId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Vendor { get; set; }
    public string Note { get; set; }
    public string Reference1 { get; set; }
    public string Reference2 { get; set; }
    public bool IsEngage { get; set; }
    public bool IsEncore { get; set; }
    public bool IsEngine { get; set; }
    public bool IsSpar { get; set; }
    public bool IsTops { get; set; }
    public List<JsonFile> FileNCircular { get; set; }
    public List<JsonFile> FileAdSpend { get; set; }
    public List<JsonFile> FileContract { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContract, SupplierContractVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractId))
               .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => new OptionDto(s.Supplier.SupplierId, s.Supplier.Name)))
               .ForMember(d => d.SupplierContractTypeId, opt => opt.MapFrom(s => s.SupplierContractType))
               .ForMember(d => d.SupplierContractGroupId, opt => opt.MapFrom(s => s.SupplierContractGroup))
               .ForMember(d => d.SupplierContractSubGroupId, opt => opt.MapFrom(s => s.SupplierContractSubGroup))
               .ForMember(d => d.SupplierContactId, opt => opt.MapFrom(s =>
                                new OptionDto(
                                    s.SupplierContact.EntityContactId,
                                    s.SupplierContact.FirstName + " " + s.SupplierContact.LastName
                                )))
               .ForMember(d => d.FileNCircular, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "ncircular")))
               .ForMember(d => d.FileAdSpend, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "adSpend")))
               .ForMember(d => d.FileContract, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "contract")));
    }
}
