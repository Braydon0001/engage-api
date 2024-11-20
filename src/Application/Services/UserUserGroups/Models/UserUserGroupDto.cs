namespace Engage.Application.Services.UserUserGroups.Models;

public class UserUserGroupDto : IMapFrom<UserUserGroup>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserEmail { get; set; }
    public int UserGroupId { get; set; }
    public string UserGroupName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserUserGroup, UserUserGroupDto>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserUserGroupId));
    }
}
