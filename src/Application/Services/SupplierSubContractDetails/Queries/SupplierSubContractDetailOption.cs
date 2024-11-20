// auto-generated
namespace Engage.Application.Services.SupplierSubContractDetails.Queries;

public class SupplierSubContractDetailOption : IMapFrom<SupplierSubContractDetail>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContractDetail, SupplierSubContractDetailOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierSubContractDetailId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Detail));
    }
}