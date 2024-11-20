namespace Engage.Application.Services.OrderSkus.Models;

public class OrderSkuListItemDto : IMapFrom<OrderSku>
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int OrderTypeId { get; set; }
    public string OrderTypeName { get; set; }
    public int OrderStatusId { get; set; }
    public string OrderStatusName { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public DateTime? SubmittedDate { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string OrderReference { get; set; }
    public int StoreId { get; set; }
    public string StoreCode { get; set; }
    public string StoreName { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public int DistributionCenterId { get; set; }
    public string DistributionCenterCode { get; set; }
    public string DistributionCenterName { get; set; }
    public string AccountNumber { get; set; }
    public string UserName { get; set; }
    public string OrderDepartments { get; set; }
    public int OrderSkuTypeId { get; set; }
    public string OrderSkuTypeName { get; set; }
    public int OrderSkuStatusId { get; set; }
    public string OrderSkuStatusName { get; set; }
    public int EngageVariantProductId { get; set; }
    public string EngageVariantProductCode { get; set; }
    public string EngageVariantProductName { get; set; }
    public int DCProductId { get; set; }
    public string DCProductCode { get; set; }
    public string ExportDCProductCode { get; set; }
    public string DCProductName { get; set; }
    public float DCProductSize { get; set; }
    public float DCProductPackSize { get; set; }
    public string DCProductUnitType { get; set; }
    public int OrderQuantityTypeId { get; set; }
    public string OrderQuantityTypeName { get; set; }
    public int Quantity { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    public string Suffix { get; set; }
    public string SupplierAccountNumber { get; set; }
    public string ProductSupplierName { get; set; }
    public string IsDropShipment { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<JsonFile> EngageVariantProductFiles { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderSku, OrderSkuListItemDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderSkuId))
            .ForMember(d => d.OrderTypeId, opt => opt.MapFrom(s => s.Order.OrderTypeId))
            .ForMember(d => d.OrderTypeName, opt => opt.MapFrom(s => s.Order.OrderType.Name))
            .ForMember(d => d.OrderStatusId, opt => opt.MapFrom(s => s.Order.OrderStatusId))
            .ForMember(d => d.OrderStatusName, opt => opt.MapFrom(s => s.Order.OrderStatus.Name))
            .ForMember(d => d.OrderDate, opt => opt.MapFrom(s => s.Order.OrderDate))
            .ForMember(d => d.DeliveryDate, opt => opt.MapFrom(s => s.DeliveryDate.HasValue ? s.DeliveryDate.Value.Date : s.Order.DeliveryDate.Value.Date))
            .ForMember(d => d.SubmittedDate, opt => opt.MapFrom(s => s.Order.SubmittedDate))
            .ForMember(d => d.OrderReference, opt => opt.MapFrom(s => s.Order.OrderReference))
            .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.Order.StoreId))
            .ForMember(d => d.StoreCode, opt => opt.MapFrom(s => s.Order.Store.Code))
            .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Order.Store.Name))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Order.SupplierId))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Order.Supplier.Name))
            .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => s.Order.Store.EngageRegionId))
            .ForMember(d => d.EngageRegionName, opt => opt.MapFrom(s => s.Order.Store.EngageRegion.Name))
            .ForMember(d => d.DistributionCenterId, opt => opt.MapFrom(s => s.Order.DistributionCenterId))
            .ForMember(d => d.DistributionCenterCode, opt => opt.MapFrom(s => s.Order.DistributionCenter.Code))
            .ForMember(d => d.DistributionCenterName, opt => opt.MapFrom(s => s.Order.DistributionCenter.Name))
            .ForMember(d => d.AccountNumber, opt => opt.MapFrom(s => s.Order.DCAccountNo))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Order.CreatedBy))
            .ForMember(d => d.OrderDepartments, opt => opt.MapFrom(s => string.Join(", ", s.Order.OrderEngageDepartments.Select(s => s.EngageDepartment.Name))))
            .ForMember(d => d.OrderSkuTypeName, opt => opt.MapFrom(s => s.OrderSkuType.Name))
            .ForMember(d => d.OrderSkuStatusName, opt => opt.MapFrom(s => s.OrderSkuStatus.Name))
            .ForMember(d => d.ProductSupplierName, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.EngageMasterProduct.Supplier.Name))
            .ForMember(d => d.EngageVariantProductId, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProductId))
            .ForMember(d => d.EngageVariantProductCode, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.Code))
            .ForMember(d => d.EngageVariantProductName, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.Name))
            .ForMember(d => d.DCProductSize, opt => opt.MapFrom(s => s.DCProduct.Size))
            .ForMember(d => d.DCProductPackSize, opt => opt.MapFrom(s => s.DCProduct.PackSize))
            .ForMember(d => d.DCProductUnitType, opt => opt.MapFrom(s => s.DCProduct.UnitType.Name))
            .ForMember(d => d.IsDropShipment, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.EngageMasterProduct.IsDropShipment ? "Yes" : ""))
            .ForMember(d => d.DCProductCode, opt => opt.MapFrom(s => s.DCProduct.Code))
            .ForMember(d => d.ExportDCProductCode, opt => opt.MapFrom(s => StringUtils.RemoveDCProductCodeSuffix(s.DCProduct.Code)))
            .ForMember(d => d.DCProductName, opt => opt.MapFrom(s => s.DCProduct.Name))
            .ForMember(d => d.DCProductSize, opt => opt.MapFrom(s => s.DCProduct.Size))
            .ForMember(d => d.DCProductPackSize, opt => opt.MapFrom(s => s.DCProduct.PackSize))
            .ForMember(d => d.OrderQuantityTypeName, opt => opt.MapFrom(s => s.OrderQuantityType.Name))
            .ForMember(d => d.EngageVariantProductFiles, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.Files))
            .ForMember(d => d.SupplierAccountNumber, opt => opt.MapFrom(s => s.Order
                                                                            .Store
                                                                            .SupplierStores
                                                                                .FirstOrDefault(su =>
                                                                                    su.StoreId == s.Order.StoreId &&
                                                                                    su.SupplierId == s.DCProduct
                                                                                        .EngageVariantProduct
                                                                                        .EngageMasterProduct
                                                                                        .SupplierId)
                                                                                    .AccountNumber ?? ""
                                                                                ));
    }
}
