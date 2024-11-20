namespace Engage.Application.Services.OrderStagings.Queries;
public class OrderStagingEmailVm : IMapFrom<OrderStaging>
{
    public int ID { get; set; }
    public string Email { get; set; }
    public string StoreName { get; set; }
    public string OrderDate { get; set; }
    public string EngageLogo { get; set; }
    public string OrderPlacedBy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderStaging, OrderStagingEmailVm>()
            .ForMember(d => d.ID, opt => opt.MapFrom(s => s.OrderStagingId))
            .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.StoreName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.OrderContactEmail))
            .ForMember(d => d.OrderDate, opt => opt.MapFrom(s => s.Date))
            .ForMember(d => d.EngageLogo, opt => opt.Ignore());
    }
}