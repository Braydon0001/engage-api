// auto-generated
namespace Engage.Application.Services.SupplierSubContractDetails.Queries;

public class SupplierSubContractDetailDto : IMapFrom<SupplierSubContractDetail>
{
    public int Id { get; set; }
    public int SupplierSubContractTypeId { get; set; }
    public string SupplierSubContractTypeName { get; set; }
    public string Detail { get; set; }
    public string Note { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContractDetail, SupplierSubContractDetailDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierSubContractDetailId));
    }
}
