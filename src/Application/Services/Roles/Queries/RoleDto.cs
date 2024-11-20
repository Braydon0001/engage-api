namespace Engage.Application.Services.Roles.Queries;

public class RoleDto : IMapFrom<Role>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string UserGroups { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Role, RoleDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.RoleId))
               .ForMember(d => d.UserGroups, opt => opt.MapFrom(s => s.RoleUserGroups.Select(g => g.UserGroup.Name).StringJoin(", ")));
    }
}
