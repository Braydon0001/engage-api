// auto-generated
namespace Engage.Application.Services.SupplierContractSplits.Queries;

public class SupplierContractSplitOption : IMapFrom<SupplierContractSplit>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractSplit, SupplierContractSplitOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractSplitId));
    }
}