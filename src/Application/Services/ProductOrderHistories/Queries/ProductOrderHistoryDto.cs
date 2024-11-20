namespace Engage.Application.Services.ProductOrderHistories.Queries;

public class ProductOrderHistoryDto : IMapFrom<ProductOrderHistory>
{
    public int Id { get; init; }
    public int ProductOrderId { get; init; }
    public string ProductOrderOrderNumber { get; init; }
    public int ProductOrderStatusId { get; init; }
    public string ProductOrderStatusName { get; init; }
    public string Reason { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderHistory, ProductOrderHistoryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderHistoryId));
    }
}
