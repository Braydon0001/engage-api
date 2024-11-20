
using Engage.Application.Services.Roles.Queries;

namespace Engage.Application.Services.RoleUserGroups.Queries;

public class RoleUserGroupVm : IMapFrom<RoleUserGroup>
{
    public int Id { get; init; }
    public RoleOption RoleId { get; init; }
    public OptionDto UserGroupId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RoleUserGroup, RoleUserGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.RoleUserGroupId))
               .ForMember(d => d.RoleId, opt => opt.MapFrom(s => s.Role))
               .ForMember(d => d.UserGroupId, opt => opt.MapFrom(s => s.UserGroup));
    }
}
