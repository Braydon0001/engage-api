namespace Engage.Application.Services.Roles.Queries;

public class RolePermissionVm : IMapFrom<Role>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public List<RolePermission> Permissions { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Role, RolePermissionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.RoleId));
    }
}

public class RolePermission : IMapFrom<UserGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool InRole { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserGroup, RolePermission>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserGroupId));
    }
}
