namespace Engage.Application.Services.SecurityPermissions.Queries;

public class SecurityPermissionDto : IMapFrom<SecurityPermission>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityPermission, SecurityPermissionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SecurityPermissionId));
    }
}
