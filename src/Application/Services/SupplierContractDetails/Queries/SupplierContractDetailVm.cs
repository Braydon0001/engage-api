// auto-generated
using Engage.Application.Services.SupplierContractDetailTypes.Queries;
using Engage.Application.Services.SupplierContracts.Queries;

namespace Engage.Application.Services.SupplierContractDetails.Queries;

public class SupplierContractDetailVm : IMapFrom<SupplierContractDetail>
{
    public int Id { get; set; }
    public SupplierContractOption SupplierContractId { get; set; }
    public SupplierContractDetailTypeOption SupplierContractDetailTypeId { get; set; }
    public OptionDto EngageRegionId { get; set; }
    public string Name { get; set; }
    public float Amount { get; set; }
    public float? RangeStartAmount { get; set; }
    public float? RangeEndAmount { get; set; }
    public string GlCode { get; set; }
    public string GlSubCode { get; set; }
    public string Note { get; set; }
    public string Reference1 { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractDetail, SupplierContractDetailVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractDetailId))
               .ForMember(d => d.SupplierContractId, opt => opt.MapFrom(s => s.SupplierContract))
               .ForMember(d => d.SupplierContractDetailTypeId, opt => opt.MapFrom(s => s.SupplierContractDetailType))
               .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => new OptionDto(s.EngageRegion.Id, s.EngageRegion.Name)));
    }
}
