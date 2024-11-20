// auto-generated
namespace Engage.Application.Services.SupplierContractTypes.Queries;

public class SupplierContractTypeVm : IMapFrom<SupplierContractType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractType, SupplierContractTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractTypeId));
    }
}
