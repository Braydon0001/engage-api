namespace Engage.Application.Services.Orders.Models;

public class OrderEmailVm : IMapFrom<Order>
{
    public int ID { get; set; }
    public string Email { get; set; }
    public string StoreName { get; set; }
    public DateTime OrderDate { get; set; }
    public string EngageLogo { get; set; }
    public string OrderPlacedBy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderEmailVm>()
            .ForMember(d => d.ID, opt => opt.MapFrom(s => s.OrderId))
            .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
            .ForMember(d => d.EngageLogo, opt => opt.Ignore());
    }
}