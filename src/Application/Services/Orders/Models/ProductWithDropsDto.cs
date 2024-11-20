namespace Engage.Application.Services.Orders.Models;

public class ProductWithDropsDto : IMapFrom<OrderSku>
{
    public int EngageVariantProductId { get; set; }
    public List<ProductDrop> Drops { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderSku, ProductWithDropsDto>()
            .ForMember(dest => dest.EngageVariantProductId, opt => opt.MapFrom(d => d.OrderSkuId));
    }
}

public class ProductDrop
{

    public int Quantity { get; set; }
    public string QuantityType { get; set; }
    public int OrderQuantityTypeId { get; set; }
    public string Note { get; set; }
    public DateTime? DeliveryDate { get; set; }

}
