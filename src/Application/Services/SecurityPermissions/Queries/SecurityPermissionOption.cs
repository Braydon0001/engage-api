namespace Engage.Application.Services.SecurityPermissions.Queries;

public class SecurityPermissionOption : IMapFrom<SecurityPermission>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityPermission, SecurityPermissionOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SecurityPermissionId));
    }
}