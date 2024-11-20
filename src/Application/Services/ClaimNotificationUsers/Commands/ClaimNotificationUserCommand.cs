namespace Engage.Application.Services.ClaimNotificationUsers.Commands;

public class ClaimNotificationUserCommand : IMapTo<ClaimNotificationUser>
{
    public int ClaimStatusId { get; set; }
    public int UserId { get; set; }
    public int EngageRegionId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimNotificationUserCommand, ClaimNotificationUser>();
    }
}
