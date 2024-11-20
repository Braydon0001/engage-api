// auto-generated
namespace Engage.Application.Services.SupplierContractSubGroups.Queries;

public class SupplierContractSubGroupOption : IMapFrom<SupplierContractSubGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractSubGroup, SupplierContractSubGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractSubGroupId));
    }
}