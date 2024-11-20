// auto-generated
namespace Engage.Application.Services.SupplierContractDetails.Queries;

public class SupplierContractDetailDto : IMapFrom<SupplierContractDetail>
{
    public int Id { get; set; }
    public int SupplierContractId { get; set; }
    public string SupplierContractName { get; set; }
    public int SupplierContractDetailTypeId { get; set; }
    public string SupplierContractDetailTypeName { get; set; }
    public int? EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
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
        profile.CreateMap<SupplierContractDetail, SupplierContractDetailDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractDetailId));
    }
}
