namespace Engage.Application.Services.SecurityRoles.Queries;

public class SecurityRoleOption : IMapFrom<SecurityRole>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityRole, SecurityRoleOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SecurityRoleId));
    }
}