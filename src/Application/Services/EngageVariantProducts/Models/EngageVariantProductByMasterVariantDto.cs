namespace Engage.Application.Services.EngageVariantProducts.Models;

public class EngageVariantProductByMasterVariantDto : IMapFrom<EngageVariantProduct>
{
    public int Id { get; set; }
    public int EngageMasterProductId { get; set; }
    public int UnitTypeId { get; set; }
    public string UnitTypeName { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public float Size { get; set; }
    public float PackSize { get; set; }
    public string EANNumber { get; set; }
    public string UnitBarcode { get; set; }
    public string CaseBarcode { get; set; }
    public string ShrinkBarcode { get; set; }
    public bool IsMaster { get; set; }
    public bool Disabled { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageVariantProduct, EngageVariantProductByMasterVariantDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageVariantProductId))
            .ForMember(d => d.IsMaster, opt => opt.MapFrom(s => s.IsMaster));
    }
}
