namespace Engage.Application.Services.OrderSkus.Queries;

public class OrderSkuProductVm : IMapFrom<OrderSku>
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int OrderSkuTypeId { get; set; }
    public string OrderSkuTypeName { get; set; }
    public int OrderSkuStatusId { get; set; }
    public string OrderSkuStatusName { get; set; }
    public int DCProductId { get; set; }
    public string DCProductCode { get; set; }
    public string DCProductName { get; set; }
    public float DCProductPackSize { get; set; }
    public float DCProductSize { get; set; }
    public string DCProductUnitType { get; set; }
    public int EngageVariantProductId { get; set; }
    public string EngageVariantProductCode { get; set; }
    public string EngageVariantProductName { get; set; }
    public List<JsonFile> EngageVariantProductFiles { get; set; }
    public int OrderQuantityTypeId { get; set; }
    public string OrderQuantityTypeName { get; set; }
    public int? OrderTemplateProductId { get; set; }
    public string OrderTemplateProductName { get; set; }
    public int OrderTemplateGroupId { get; set; }
    public string OrderTemplateGroupName { get; set; }
    public int OrderTemplateGroupOrder { get; set; }
    public string IsDropShipment { get; set; }
    public int Quantity { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal PromotionPrice { get; set; }
    public decimal RecommendedPrice { get; set; }
    public decimal GrossProfitPercent { get; set; }
    public string Suffix { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderSku, OrderSkuProductVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderSkuId))
               .ForMember(d => d.DCProductUnitType, opt => opt.MapFrom(s => s.DCProduct.UnitType.Name))
               .ForMember(d => d.EngageVariantProductId, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProductId))
               .ForMember(d => d.EngageVariantProductCode, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.Code))
               .ForMember(d => d.EngageVariantProductName, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.Name))
               .ForMember(d => d.EngageVariantProductFiles, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.Files))
               .ForMember(d => d.OrderTemplateGroupId, opt => opt.MapFrom(s => s.OrderTemplateProduct.OrderTemplateGroupId))
               .ForMember(d => d.OrderTemplateGroupName, opt => opt.MapFrom(s => s.OrderTemplateProduct.OrderTemplateGroup.Name))
               .ForMember(d => d.OrderTemplateGroupOrder, opt => opt.MapFrom(s => s.OrderTemplateProduct.OrderTemplateGroup.Order))
               .ForMember(d => d.IsDropShipment, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.EngageMasterProduct.IsDropShipment ? "Yes" : ""));

    }
}

