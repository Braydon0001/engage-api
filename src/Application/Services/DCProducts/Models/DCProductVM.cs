namespace Engage.Application.Services.DCProducts.Models;

public class DCProductVm : IMapFrom<DCProduct>
{
    public int Id { get; set; }
    public OptionDto EngageVariantProductId { get; set; }
    public OptionDto DistributionCenterId { get; set; }
    public OptionDto VendorId { get; set; }
    public OptionDto ManufacturerId { get; set; }
    public OptionDto ProductClassId { get; set; }
    public OptionDto UnitTypeId { get; set; }
    public OptionDto ProductActiveStatusId { get; set; }
    public OptionDto ProductStatusId { get; set; }
    public OptionDto ProductWarehouseStatusId { get; set; }
    public OptionDto ProductSubWarehouseId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public float Size { get; set; }
    public float PackSize { get; set; }
    public string EANNumber { get; set; }
    public string SubWarehouse { get; set; }

    public List<JsonFile> Files { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DCProduct, DCProductVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.DCProductId))
            .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.Code + " / " + s.Name + " / " + s.Size + " " + s.UnitType.Name))
            .ForMember(d => d.EngageVariantProductId, opt => opt.MapFrom(s => new OptionDto(s.EngageVariantProductId.Value, s.EngageVariantProduct.Name)))
            .ForMember(d => d.DistributionCenterId, opt => opt.MapFrom(s => new OptionDto(s.DistributionCenterId, s.DistributionCenter.Name)))
            .ForMember(d => d.VendorId, opt => opt.MapFrom(s => new OptionDto(s.VendorId, s.Vendor.Name)))
            .ForMember(d => d.ManufacturerId, opt => opt.MapFrom(s => s.ManufacturerId.HasValue ? new OptionDto(s.ManufacturerId.Value, s.Manufacturer.Name) : null))
            .ForMember(d => d.ProductClassId, opt => opt.MapFrom(s => new OptionDto(s.ProductClassId, s.ProductClass.Name)))
            .ForMember(d => d.UnitTypeId, opt => opt.MapFrom(s => new OptionDto(s.UnitTypeId, s.UnitType.Name)))
            .ForMember(d => d.ProductActiveStatusId, opt => opt.MapFrom(s => new OptionDto(s.ProductActiveStatusId, s.ProductActiveStatus.Name)))
            .ForMember(d => d.ProductStatusId, opt => opt.MapFrom(s => new OptionDto(s.ProductStatusId, s.ProductStatus.Name)))
            .ForMember(d => d.ProductWarehouseStatusId, opt => opt.MapFrom(s => new OptionDto(s.ProductWarehouseStatusId, s.ProductWarehouseStatus.Name)))
            .ForMember(d => d.ProductSubWarehouseId, opt => opt.MapFrom(s => new OptionDto(s.ProductSubWarehouseId, s.ProductSubWarehouse.Name)));
    }
}
