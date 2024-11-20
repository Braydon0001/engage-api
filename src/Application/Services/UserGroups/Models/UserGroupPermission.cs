namespace Engage.Application.Services.UserGroups.Models;
public class UserGroupPermission : IMapFrom<UserGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Assigned { get; set; }
    public bool FromRole { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserGroup, UserGroupPermission>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserGroupId));
    }
}
