namespace Engage.Application.Services.DCProducts.Models;

public class DCProductListDto : IMapFrom<DCProduct>
{
    public int Id { get; set; }
    public int EngageVariantProductId { get; set; }
    public string EngageVariantProductCode { get; set; }
    public string EngageVariantProductName { get; set; }
    public bool EngageVariantProductDisabled { get; set; }
    public bool EngageVariantProductDeleted { get; set; }
    public int EngageMasterProductId { get; set; }
    public string EngageMasterProductCode { get; set; }
    public string EngageMasterProductName { get; set; }
    public string EngageMasterProductSubCategoryName { get; set; }
    public bool EngageMasterProductDisabled { get; set; }
    public bool EngageMasterProductDeleted { get; set; }
    public int DistributionCenterId { get; set; }
    public string DistributionCenter { get; set; }
    public int VendorId { get; set; }
    public string VendorName { get; set; }
    public int? ManufacturerId { get; set; }
    public string ManufacturerName { get; set; }
    public int ProductClassId { get; set; }
    public string ProductClassName { get; set; }
    public int UnitTypeId { get; set; }
    public string UnitTypeName { get; set; }
    public int ProductActiveStatusId { get; set; }
    public string ProductActiveStatusName { get; set; }
    public int ProductStatusId { get; set; }
    public string ProductStatusName { get; set; }
    public int ProductWarehouseStatusId { get; set; }
    public string ProductWarehouseStatusName { get; set; }
    public int ProductSubWarehouseId { get; set; }
    public string ProductSubWarehouseName { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public float Size { get; set; }
    public float PackSize { get; set; }
    public string EANNumber { get; set; }
    public bool Disabled { get; set; }
    public bool Deleted { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DCProduct, DCProductListDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.DCProductId))
            .ForMember(e => e.EngageVariantProductCode, opt => opt.MapFrom(d => d.EngageVariantProduct.Code))
            .ForMember(d => d.EngageVariantProductName, opt => opt.MapFrom(d => d.EngageVariantProduct.Name))
            .ForMember(d => d.EngageVariantProductDisabled, opt => opt.MapFrom(d => d.EngageVariantProduct.Disabled))
            .ForMember(d => d.EngageVariantProductDeleted, opt => opt.MapFrom(d => d.EngageVariantProduct.Deleted))
            .ForMember(d => d.EngageMasterProductId, opt => opt.MapFrom(s => s.EngageVariantProduct.EngageMasterProductId))
            .ForMember(d => d.EngageMasterProductCode, opt => opt.MapFrom(s => s.EngageVariantProduct.EngageMasterProduct.Code))
            .ForMember(d => d.EngageMasterProductName, opt => opt.MapFrom(s => s.EngageVariantProduct.EngageMasterProduct.Name))
            .ForMember(d => d.EngageMasterProductSubCategoryName, opt => opt.MapFrom(s => s.EngageVariantProduct.EngageMasterProduct.EngageSubCategory.Name))
            .ForMember(d => d.EngageMasterProductDisabled, opt => opt.MapFrom(s => s.EngageVariantProduct.EngageMasterProduct.Disabled))
            .ForMember(d => d.EngageMasterProductDeleted, opt => opt.MapFrom(s => s.EngageVariantProduct.EngageMasterProduct.Deleted))
            .ForMember(d => d.DistributionCenter, opt => opt.MapFrom(s => s.DistributionCenter.Name))
            .ForMember(d => d.VendorName, opt => opt.MapFrom(s => s.Vendor.Name))
            .ForMember(d => d.ProductClassName, opt => opt.MapFrom(s => s.ProductClass.Name))
            .ForMember(d => d.UnitTypeName, opt => opt.MapFrom(s => s.UnitType.Name))
            .ForMember(d => d.ProductActiveStatusName, opt => opt.MapFrom(s => s.ProductActiveStatus.Name))
            .ForMember(d => d.ProductStatusName, opt => opt.MapFrom(s => s.ProductStatus.Name))
            .ForMember(d => d.ProductWarehouseStatusName, opt => opt.MapFrom(s => s.ProductWarehouseStatus.Name))
            .ForMember(d => d.ProductSubWarehouseName, opt => opt.MapFrom(s => s.ProductSubWarehouse.Name));
    }
}
