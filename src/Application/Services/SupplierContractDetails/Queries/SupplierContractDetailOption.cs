// auto-generated
namespace Engage.Application.Services.SupplierContractDetails.Queries;

public class SupplierContractDetailOption : IMapFrom<SupplierContractDetail>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractDetail, SupplierContractDetailOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractDetailId));
    }
}