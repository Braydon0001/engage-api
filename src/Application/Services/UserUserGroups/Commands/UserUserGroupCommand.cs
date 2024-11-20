namespace Engage.Application.Services.UserUserGroups.Commands;

public class UserUserGroupCommand : IMapTo<UserUserGroup>
{
    public int UserId { get; set; }
    public int UserGroupId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserUserGroupCommand, UserUserGroup>();
    }
}
