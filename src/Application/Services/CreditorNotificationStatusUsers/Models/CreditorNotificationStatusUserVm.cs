namespace Engage.Application.Services.CreditorNotificationStatusUsers.Models;

public class CreditorNotificationStatusUserVm : IMapFrom<CreditorNotificationStatusUser>
{
    public int Id { get; set; }
    public OptionDto ClaimStatusId { get; set; }
    public OptionDto UserId { get; set; }
    public OptionDto EngageRegionId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorNotificationStatusUser, CreditorNotificationStatusUserVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorNotificationStatusUserId))
            .ForMember(d => d.ClaimStatusId, opt => opt.MapFrom(s => new OptionDto(s.CreditorStatus.CreditorStatusId, s.CreditorStatus.Name)))
            .ForMember(d => d.UserId, opt => opt.MapFrom(s => new OptionDto(s.UserId, $"{s.User.FirstName} {s.User.LastName}")))
            .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => s.EngageRegionId.HasValue ? new OptionDto(s.EngageRegionId.Value, s.EngageRegion.Name) : null));
    }
}
