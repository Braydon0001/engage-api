// auto-generated
using Engage.Application.Services.SupplierContractGroups.Queries;

namespace Engage.Application.Services.SupplierContractSubGroups.Queries;

public class SupplierContractSubGroupVm : IMapFrom<SupplierContractSubGroup>
{
    public int Id { get; set; }
    public SupplierContractGroup SupplierContractGroupId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractSubGroup, SupplierContractSubGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractSubGroupId))
               .ForMember(d => d.SupplierContractGroupId, opt => opt.MapFrom(s => s.SupplierContractGroup));
    }
}
