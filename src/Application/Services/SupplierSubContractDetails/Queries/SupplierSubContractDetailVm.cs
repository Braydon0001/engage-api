// auto-generated
using Engage.Application.Services.SupplierSubContractDetailTypes.Queries;
using Engage.Application.Services.SupplierSubContractTypes.Queries;

namespace Engage.Application.Services.SupplierSubContractDetails.Queries;

public class SupplierSubContractDetailVm : IMapFrom<SupplierSubContractDetail>
{
    public int Id { get; set; }
    public SupplierSubContractTypeOption SupplierSubContractTypeId { get; set; }
    public SupplierSubContractDetailTypeOption SupplierSubContractDetailTypeId { get; set; }
    public string Detail { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContractDetail, SupplierSubContractDetailVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierSubContractDetailId))
               .ForMember(d => d.SupplierSubContractTypeId, opt => opt.MapFrom(s => s.SupplierSubContractType))
               .ForMember(d => d.SupplierSubContractDetailTypeId, opt => opt.MapFrom(s => s.SupplierSubContractDetailType));
    }
}
