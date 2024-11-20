namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuCommand : IMapTo<OrderSku>
{
    public int OrderSkuTypeId { get; set; }
    public int OrderSkuStatusId { get; set; }
    public int DCProductId { get; set; }
    public int OrderQuantityTypeId { get; set; }
    public int Quantity { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderSkuCommand, OrderSku>();
    }
}
