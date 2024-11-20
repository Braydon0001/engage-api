namespace Engage.Application.Services.UserPermissions.Queries;

public class UserPermissionOption : IMapFrom<UserPermission>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserPermission, UserPermissionOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserPermissionId));
    }
}