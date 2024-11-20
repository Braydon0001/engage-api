namespace Engage.Application.Services.UserRolePermissions.Queries;

public class UserRolePermissionOption : IMapFrom<UserRolePermission>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRolePermission, UserRolePermissionOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserRolePermissionId));
    }
}