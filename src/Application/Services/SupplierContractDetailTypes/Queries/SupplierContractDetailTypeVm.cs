// auto-generated
namespace Engage.Application.Services.SupplierContractDetailTypes.Queries;

public class SupplierContractDetailTypeVm : IMapFrom<SupplierContractDetailType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractDetailType, SupplierContractDetailTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractDetailTypeId));
    }
}
