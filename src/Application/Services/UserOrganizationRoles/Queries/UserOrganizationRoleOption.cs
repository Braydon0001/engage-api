namespace Engage.Application.Services.UserOrganizationRoles.Queries;

public class UserOrganizationRoleOption : IMapFrom<UserOrganizationRole>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserOrganizationRole, UserOrganizationRoleOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserOrganizationRoleId));
    }
}