namespace Engage.Application.Services.PaymentNotificationStatusUsers.Models;

public class PaymentNotificationStatusUserDto : IMapFrom<PaymentNotificationStatusUser>
{
    public int Id { get; set; }
    public int PaymentStatusId { get; set; }
    public string PaymentStatusName { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public string Email { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentNotificationStatusUser, PaymentNotificationStatusUserDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentNotificationStatusUserId))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => $"{s.User.FirstName} {s.User.LastName} - {s.User.Email}"))
            .ForMember(d => d.PaymentStatusName, opt => opt.MapFrom(s => s.PaymentStatus.Name))
            .ForMember(d => d.EngageRegionName, opt => opt.MapFrom(s => s.EngageRegion.Name))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email));
    }

}
