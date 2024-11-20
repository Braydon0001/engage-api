
namespace Engage.Application.Services.Roles.Queries;

public class RoleVm : IMapFrom<Role>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public List<OptionDto> UserGroupIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Role, RoleVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.RoleId))
               .ForMember(d => d.UserGroupIds, opt => opt.MapFrom(s => s.RoleUserGroups.Select(r => new OptionDto() { Id = r.UserGroupId, Name = r.UserGroup.Name })));
    }
}
