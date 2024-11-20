namespace Engage.Application.Services.Orders.Models;

public class ProductDto : IMapFrom<OrderSku>
{
    public int EngageVariantProductId { get; set; }
    public int OrderQuantityTypeId { get; set; }
    public int Quantity { get; set; }
    public string Note { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderSku, ProductDto>()
            .ForMember(dest => dest.EngageVariantProductId, opt => opt.MapFrom(d => d.OrderSkuId))
            .ForMember(dest => dest.OrderQuantityTypeId, opt => opt.MapFrom(d => d.OrderQuantityTypeId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(d => d.Quantity))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(d => d.Note));
    }
}
