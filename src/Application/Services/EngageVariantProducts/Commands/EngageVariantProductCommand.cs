namespace Engage.Application.Services.EngageVariantProducts.Commands;

public class EngageVariantProductCommand : IMapTo<EngageVariantProduct>
{
    public int UnitTypeId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public float Size { get; set; }
    public float PackSize { get; set; }
    public string EANNumber { get; set; }
    public string UnitBarcode { get; set; }
    public string CaseBarcode { get; set; }
    public string ShrinkBarcode { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<EngageVariantProductCommand, EngageVariantProduct>();
    }
}
