namespace Engage.Application.Services.PaymentNotificationStatusUsers.Models;

public class PaymentNotificationStatusUserVm : IMapFrom<PaymentNotificationStatusUser>
{
    public int Id { get; set; }
    public OptionDto ClaimStatusId { get; set; }
    public OptionDto UserId { get; set; }
    public OptionDto EngageRegionId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentNotificationStatusUser, PaymentNotificationStatusUserVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentNotificationStatusUserId))
            .ForMember(d => d.ClaimStatusId, opt => opt.MapFrom(s => new OptionDto(s.PaymentStatus.PaymentStatusId, s.PaymentStatus.Name)))
            .ForMember(d => d.UserId, opt => opt.MapFrom(s => new OptionDto(s.UserId, $"{s.User.FirstName} {s.User.LastName}")))
            .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => new OptionDto(s.EngageRegionId, s.EngageRegion.Name)));
    }
}
