
using Engage.Application.Services.UserRoles.Queries;
using Engage.Application.Services.UserPermissions.Queries;

namespace Engage.Application.Services.UserRolePermissions.Queries;

public class UserRolePermissionVm : IMapFrom<UserRolePermission>
{
    public int Id { get; init; }
    public UserRoleOption UserRoleId { get; init; }
    public UserPermissionOption UserPermissionId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRolePermission, UserRolePermissionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserRolePermissionId))
               .ForMember(d => d.UserRoleId, opt => opt.MapFrom(s => s.UserRole))
               .ForMember(d => d.UserPermissionId, opt => opt.MapFrom(s => s.UserPermission));
    }
}
