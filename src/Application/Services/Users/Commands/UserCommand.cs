namespace Engage.Application.Services.Users.Commands;

public class UserCommand : IMapTo<User>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string MobilePhone { get; set; }
    public int SupplierId { get; set; }
    public bool IgnoreOrderProductFilters { get; set; }
    public int? RoleId { get; set; }
    public bool? SkipOktaActions { get; set; } = false;
    public List<int> EngageRegionIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserCommand, User>();
    }
}
