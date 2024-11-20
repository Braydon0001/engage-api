// auto-generated
using Engage.Application.Services.SupplierContracts.Queries;
using Engage.Application.Services.SupplierSubContractTypes.Queries;

namespace Engage.Application.Services.SupplierSubContracts.Queries;

public class SupplierSubContractVm : IMapFrom<SupplierSubContract>
{
    public int Id { get; set; }
    public SupplierContractOption SupplierContractId { get; set; }
    public SupplierSubContractTypeOption SupplierSubContractTypeId { get; set; }
    public string Name { get; set; }
    public string Reference1 { get; set; }
    public string GlMainCode { get; set; }
    public string GlSubCode { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContract, SupplierSubContractVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierSubContractId))
               .ForMember(d => d.SupplierContractId, opt => opt.MapFrom(s => s.SupplierContract))
               .ForMember(d => d.SupplierSubContractTypeId, opt => opt.MapFrom(s => s.SupplierSubContractType));
    }
}
