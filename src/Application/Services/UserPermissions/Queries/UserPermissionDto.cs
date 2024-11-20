namespace Engage.Application.Services.UserPermissions.Queries;

public class UserPermissionDto : IMapFrom<UserPermission>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserPermission, UserPermissionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserPermissionId));
    }
}
