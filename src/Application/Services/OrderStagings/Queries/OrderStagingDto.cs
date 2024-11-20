namespace Engage.Application.Services.OrderStagings.Queries;

public class OrderStagingDto : IMapFrom<OrderStaging>
{
    public int Id { get; init; }
    public string Region { get; init; }
    public string StoreName { get; init; }
    public string AccountNumber { get; init; }
    public string OrderNumber { get; init; }
    public string OrderContactName { get; init; }
    public string OrderContactEmail { get; init; }
    public string VatNumber { get; init; }
    public string OrderDate { get; init; }
    public string Reference { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderStaging, OrderStagingDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderStagingId))
               .ForMember(d => d.OrderDate, opt => opt.MapFrom(s => s.Date));
    }
}
