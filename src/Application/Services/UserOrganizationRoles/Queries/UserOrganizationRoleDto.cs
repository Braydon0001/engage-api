namespace Engage.Application.Services.UserOrganizationRoles.Queries;

public class UserOrganizationRoleDto : IMapFrom<UserOrganizationRole>
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public string UserFullName { get; init; }
    public int UserOrganizationId { get; init; }
    public string UserOrganizationName { get; init; }
    public int UserRoleId { get; init; }
    public string UserRoleName { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserOrganizationRole, UserOrganizationRoleDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserOrganizationRoleId));
    }
}
