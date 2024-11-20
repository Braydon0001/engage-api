namespace Engage.Application.Services.ProductOrders.Queries;

public class ProductOrderOption : IMapFrom<ProductOrder>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrder, ProductOrderOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.OrderNumber));
    }
}