namespace Engage.Application.Services.UserRoles.Queries;

public class UserRoleOption : IMapFrom<UserRole>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRole, UserRoleOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserRoleId));
    }
}