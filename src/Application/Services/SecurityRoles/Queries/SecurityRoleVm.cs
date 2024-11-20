
namespace Engage.Application.Services.SecurityRoles.Queries;

public class SecurityRoleVm : IMapFrom<SecurityRole>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public List<OptionDto> SecurityPermissionIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityRole, SecurityRoleVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SecurityRoleId))
               .ForMember(d => d.SecurityPermissionIds, opt =>
                    opt.MapFrom(s => s.SecurityPermissionRoles.Select(o =>
                    new OptionDto { Id = o.SecurityPermissionId, Name = o.SecurityPermission.Name }).ToList()));
    }
}
