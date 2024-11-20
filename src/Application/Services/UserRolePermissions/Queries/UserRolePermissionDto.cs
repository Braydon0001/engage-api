namespace Engage.Application.Services.UserRolePermissions.Queries;

public class UserRolePermissionDto : IMapFrom<UserRolePermission>
{
    public int Id { get; init; }
    public int UserRoleId { get; init; }
    public string UserRoleName { get; init; }
    public int UserPermissionId { get; init; }
    public string UserPermisionName { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRolePermission, UserRolePermissionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserRolePermissionId));
    }
}
