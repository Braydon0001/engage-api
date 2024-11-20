// auto-generated
namespace Engage.Application.Services.SupplierContractTypes.Queries;

public class SupplierContractTypeOption : IMapFrom<SupplierContractType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractType, SupplierContractTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractTypeId));
    }
}