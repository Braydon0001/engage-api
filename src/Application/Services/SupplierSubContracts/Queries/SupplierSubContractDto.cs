// auto-generated
namespace Engage.Application.Services.SupplierSubContracts.Queries;

public class SupplierSubContractDto : IMapFrom<SupplierSubContract>
{
    public int Id { get; set; }
    public int SupplierContractId { get; set; }
    public string SupplierContractName { get; set; }
    public int SupplierSubContractTypeId { get; set; }
    public string SupplierSubContractTypeName { get; set; }
    public string Name { get; set; }
    public string Reference1 { get; set; }
    public string GlMainCode { get; set; }
    public string GlSubCode { get; set; }
    public string Note { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContract, SupplierSubContractDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierSubContractId));
    }
}
