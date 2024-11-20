// auto-generated
namespace Engage.Application.Services.SupplierContractAmountTypes.Queries;

public class SupplierContractAmountTypeVm : IMapFrom<SupplierContractAmountType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractAmountType, SupplierContractAmountTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractAmountTypeId));
    }
}
