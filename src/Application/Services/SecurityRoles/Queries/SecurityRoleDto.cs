namespace Engage.Application.Services.SecurityRoles.Queries;

public class SecurityRoleDto : IMapFrom<SecurityRole>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public string SecurityPermissions { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityRole, SecurityRoleDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SecurityRoleId))
               .ForMember(d => d.SecurityPermissions, opt =>
                    opt.MapFrom(s => string.Join(", ", s.SecurityPermissionRoles.Select(e => e.SecurityPermission.Key).ToList())));
    }
}
