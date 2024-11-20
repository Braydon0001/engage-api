namespace Engage.Application.Services.EngageVariantProducts.Models;

public class EngageVariantProductMobileDto : IMapFrom<EngageVariantProduct>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public float Size { get; set; }
    public string UnitType { get; set; }
    public string Warehouse { get; set; }

    public void Mapping(Profile profile)
    {
    }
}
