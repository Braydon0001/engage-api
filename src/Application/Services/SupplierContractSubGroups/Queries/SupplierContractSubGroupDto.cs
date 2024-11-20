// auto-generated
namespace Engage.Application.Services.SupplierContractSubGroups.Queries;

public class SupplierContractSubGroupDto : IMapFrom<SupplierContractSubGroup>
{
    public int Id { get; set; }
    public int SupplierContractGroupId { get; set; }
    public string SupplierContractGroupName { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractSubGroup, SupplierContractSubGroupDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractSubGroupId));
    }
}
