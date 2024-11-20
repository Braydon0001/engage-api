namespace Engage.Application.Services.Orders.Models;

public class ProcessedOrderDto : IMapFrom<Order>
{
    public int Id { get; set; }
    public string ProcessedBy { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, ProcessedOrderDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderId));
    }
}
