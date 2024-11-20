namespace Engage.Application.Services.RoleUserGroups.Queries;

public class RoleUserGroupDto : IMapFrom<RoleUserGroup>
{
    public int Id { get; init; }
    public int RoleId { get; init; }
    public int UserGroupId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RoleUserGroup, RoleUserGroupDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.RoleUserGroupId));
    }
}
