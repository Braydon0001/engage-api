namespace Engage.Application.Services.DCProducts.Models;

public class DCProductDto : IMapFrom<DCProduct>
{
    public int Id { get; set; }
    public int? EngageVariantProductId { get; set; }
    public int DistributionCenterId { get; set; }
    public int VendorId { get; set; }
    public int? ManufacturerId { get; set; }
    public int ProductClassId { get; set; }
    public int UnitTypeId { get; set; }
    public int ProductActiveStatusId { get; set; }
    public int ProductStatusId { get; set; }
    public int ProductWarehouseStatusId { get; set; }
    public int ProductSubWarehouseId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public float Size { get; set; }
    public float PackSize { get; set; }
    public string EANNumber { get; set; }
    public string SubWarehouse { get; set; }
    public bool Disabled { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DCProduct, DCProductDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.DCProductId))
            .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.Code + " / " + s.Name + " / " + s.Size + " " + s.UnitType.Name));
    }
}
