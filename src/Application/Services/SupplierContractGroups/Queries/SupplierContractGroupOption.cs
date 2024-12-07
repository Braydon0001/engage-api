// auto-generated
namespace Engage.Application.Services.SupplierContractGroups.Queries;

public class SupplierContractGroupOption : IMapFrom<SupplierContractGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractGroup, SupplierContractGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractGroupId));
    }
}