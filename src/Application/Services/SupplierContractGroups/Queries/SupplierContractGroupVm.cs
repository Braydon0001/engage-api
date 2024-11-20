// auto-generated
namespace Engage.Application.Services.SupplierContractGroups.Queries;

public class SupplierContractGroupVm : IMapFrom<SupplierContractGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractGroup, SupplierContractGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractGroupId));
    }
}
