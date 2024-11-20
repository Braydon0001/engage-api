namespace Engage.Application.Services.ClaimNotificationUsers.Models;

public class ClaimNotificationUserDto : IMapFrom<ClaimNotificationUser>
{
    public int Id { get; set; }
    public int ClaimStatusId { get; set; }
    public string ClaimStatusName { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public string Email { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimNotificationUser, ClaimNotificationUserDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimNotificationUserId))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => $"{s.User.FirstName} {s.User.LastName}"))
            .ForMember(d => d.ClaimStatusName, opt => opt.MapFrom(s => s.ClaimStatus.Name))
            .ForMember(d => d.EngageRegionName, opt => opt.MapFrom(s => s.EngageRegion.Name))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email));
    }

}
