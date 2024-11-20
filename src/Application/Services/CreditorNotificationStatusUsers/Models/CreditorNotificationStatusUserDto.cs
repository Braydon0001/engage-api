namespace Engage.Application.Services.CreditorNotificationStatusUsers.Models;

public class CreditorNotificationStatusUserDto : IMapFrom<CreditorNotificationStatusUser>
{
    public int Id { get; set; }
    public int CreditorStatusId { get; set; }
    public string CreditorStatusName { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public string Email { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorNotificationStatusUser, CreditorNotificationStatusUserDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorNotificationStatusUserId))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => $"{s.User.FirstName} {s.User.LastName} - {s.User.Email}"))
            .ForMember(d => d.CreditorStatusName, opt => opt.MapFrom(s => s.CreditorStatus.Name))
            .ForMember(d => d.EngageRegionName, opt => opt.MapFrom(s => s.EngageRegion.Name))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email));
    }

}
