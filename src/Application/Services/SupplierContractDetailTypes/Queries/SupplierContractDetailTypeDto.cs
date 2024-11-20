// auto-generated
namespace Engage.Application.Services.SupplierContractDetailTypes.Queries;

public class SupplierContractDetailTypeDto : IMapFrom<SupplierContractDetailType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractDetailType, SupplierContractDetailTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractDetailTypeId));
    }
}
