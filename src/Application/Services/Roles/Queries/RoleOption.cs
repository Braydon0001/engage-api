namespace Engage.Application.Services.Roles.Queries;

public class RoleOption : IMapFrom<Role>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Role, RoleOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.RoleId));
    }
}