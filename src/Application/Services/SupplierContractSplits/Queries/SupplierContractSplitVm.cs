// auto-generated
namespace Engage.Application.Services.SupplierContractSplits.Queries;

public class SupplierContractSplitVm : IMapFrom<SupplierContractSplit>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractSplit, SupplierContractSplitVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractSplitId));
    }
}
