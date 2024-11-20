namespace Engage.Application.Services.EngageMasterProducts.Models;

public class EngageProductMasterVariantDto : IMapFrom<EngageMasterProduct>, IMapFrom<EngageVariantProduct>, IMapFrom<DCProduct>
{
    public int Id { get; set; }
    public int? EngageVariantProductId { get; set; }
    public int EngageMasterproductId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int SupplierId { get; set; }
    public string EngageSubCategoryName { get; set; }
    public string SupplierName { get; set; }
    public int EngageBrandId { get; set; }
    public string EngageBrandName { get; set; }
    public string EANNumber { get; set; }
    public string ProductActiveStatusName { get; set; }
    public string ProductStatusName { get; set; }
    public string ProductWarehouseStatusName { get; set; }
    public string ProductSubWarehouseName { get; set; }
    public bool IsParent { get; set; }
    public bool IsMasterVariant { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageMasterProduct, EngageProductMasterVariantDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageMasterProductId))
            .ForMember(d => d.EngageMasterproductId, opt => opt.MapFrom(s => s.EngageMasterProductId))
            .ForMember(d => d.IsParent, opt => opt.MapFrom(s => true));

        profile.CreateMap<EngageVariantProduct, EngageProductMasterVariantDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageMasterProductId))
            .ForMember(d => d.EngageVariantProductId, opt => opt.MapFrom(s => s.EngageVariantProductId))
            .ForMember(d => d.EngageSubCategoryName, opt => opt.MapFrom(s => s.EngageMasterProduct.EngageSubCategory.Name))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.EngageMasterProduct.SupplierId))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.EngageMasterProduct.Supplier.Name))
            .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.EngageMasterProduct.EngageBrandId))
            .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.EngageMasterProduct.EngageBrand.Name))
            .ForMember(d => d.IsMasterVariant, opt => opt.MapFrom(s => s.IsMaster))
            .ForMember(d => d.IsParent, opt => opt.MapFrom(s => true));

        profile.CreateMap<DCProduct, EngageProductMasterVariantDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.DCProductId))
            .ForMember(d => d.EngageVariantProductId, opt => opt.MapFrom(s => s.EngageVariantProductId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.ProductActiveStatusName, opt => opt.MapFrom(s => s.ProductActiveStatus.Name))
            .ForMember(d => d.ProductStatusName, opt => opt.MapFrom(s => s.ProductStatus.Name))
            .ForMember(d => d.ProductWarehouseStatusName, opt => opt.MapFrom(s => s.ProductWarehouseStatus.Name))
            .ForMember(d => d.ProductSubWarehouseName, opt => opt.MapFrom(s => s.ProductSubWarehouse.Name))
            .ForMember(d => d.EngageMasterproductId, opt => opt.MapFrom(s => s.EngageVariantProduct.EngageMasterProductId))
            .ForMember(d => d.IsParent, opt => opt.MapFrom(s => false));
    }
}

public class EngageProductMasterVariantRelatedDto : IMapFrom<EngageMasterProduct>, IMapFrom<EngageVariantProduct>, IMapFrom<DCProduct>
{
    public int Id { get; set; }
    public int? EngageVariantProductId { get; set; }
    public int EngageMasterproductId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int SupplierId { get; set; }
    public string EngageSubCategoryName { get; set; }
    public string SupplierName { get; set; }
    public int EngageBrandId { get; set; }
    public string EngageBrandName { get; set; }
    public string EANNumber { get; set; }
    public string ProductActiveStatusName { get; set; }
    public string ProductStatusName { get; set; }
    public string ProductWarehouseStatusName { get; set; }
    public string ProductSubWarehouseName { get; set; }
    public bool IsParent { get; set; }
    public bool IsMasterVariant { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageMasterProduct, EngageProductMasterVariantRelatedDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageMasterProductId))
            .ForMember(d => d.EngageMasterproductId, opt => opt.MapFrom(s => s.EngageMasterProductId))
            .ForMember(d => d.IsParent, opt => opt.MapFrom(s => true));

        profile.CreateMap<EngageVariantProduct, EngageProductMasterVariantRelatedDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageVariantProductId))
            .ForMember(d => d.EngageVariantProductId, opt => opt.MapFrom(s => s.EngageVariantProductId))
            .ForMember(d => d.EngageSubCategoryName, opt => opt.MapFrom(s => s.EngageMasterProduct.EngageSubCategory.Name))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.EngageMasterProduct.SupplierId))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.EngageMasterProduct.Supplier.Name))
            .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.EngageMasterProduct.EngageBrandId))
            .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.EngageMasterProduct.EngageBrand.Name))
            .ForMember(d => d.IsMasterVariant, opt => opt.MapFrom(s => s.IsMaster))
            .ForMember(d => d.IsParent, opt => opt.MapFrom(s => true));

        profile.CreateMap<DCProduct, EngageProductMasterVariantRelatedDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.DCProductId))
            .ForMember(d => d.EngageVariantProductId, opt => opt.MapFrom(s => s.EngageVariantProductId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.ProductActiveStatusName, opt => opt.MapFrom(s => s.ProductActiveStatus.Name))
            .ForMember(d => d.ProductStatusName, opt => opt.MapFrom(s => s.ProductStatus.Name))
            .ForMember(d => d.ProductWarehouseStatusName, opt => opt.MapFrom(s => s.ProductWarehouseStatus.Name))
            .ForMember(d => d.ProductSubWarehouseName, opt => opt.MapFrom(s => s.ProductSubWarehouse.Name))
            .ForMember(d => d.EngageMasterproductId, opt => opt.MapFrom(s => s.EngageVariantProduct.EngageMasterProductId))
            .ForMember(d => d.IsParent, opt => opt.MapFrom(s => false));
    }
}
