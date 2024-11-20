namespace Engage.Application.Services.UserRoles.Queries;

public class UserRoleDto : IMapFrom<UserRole>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Key { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRole, UserRoleDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserRoleId));
    }
}
