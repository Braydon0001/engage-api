// auto-generated
namespace Engage.Application.Services.SupplierContracts.Queries;

public class SupplierContractOption : IMapFrom<SupplierContract>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContract, SupplierContractOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractId));
    }
}