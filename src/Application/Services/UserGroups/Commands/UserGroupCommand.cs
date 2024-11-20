namespace Engage.Application.Services.UserGroups.Commands;
public class UserGroupCommand : IMapTo<UserGroup>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string VendorId { get; set; }
    public bool? SkipOktaActions { get; set; } = false;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserGroupCommand, UserGroup>();
    }
}
