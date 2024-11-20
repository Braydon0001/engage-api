// auto-generated
namespace Engage.Application.Services.SupplierSubContractTypes.Queries;

public class SupplierSubContractTypeOption : IMapFrom<SupplierSubContractType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContractType, SupplierSubContractTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierSubContractTypeId));
    }
}