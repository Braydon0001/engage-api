namespace Engage.Application.Services.SecurityRoles.Queries;

public class SecurityRoleWithPermissionsVm : IMapFrom<SecurityRole>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public List<SecurityRoleDtoPermissionVm> SecurityPermissions { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityRole, SecurityRoleWithPermissionsVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SecurityRoleId))
               .ForMember(d => d.SecurityPermissions, opt =>
                    opt.MapFrom(s => s.SecurityPermissionRoles.Select(f =>
                            new SecurityRoleDtoPermissionVm
                            {
                                Id = f.SecurityPermissionId,
                                Key = f.SecurityPermission.Key,
                                Name = f.SecurityPermission.Name,
                            }).ToList()));
    }
}

public class SecurityRoleDtoPermissionVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
}