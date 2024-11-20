
using Engage.Application.Services.Users.Queries;
using Engage.Application.Services.UserOrganizations.Queries;
using Engage.Application.Services.UserRoles.Queries;

namespace Engage.Application.Services.UserOrganizationRoles.Queries;

public class UserOrganizationRoleVm : IMapFrom<UserOrganizationRole>
{
    public int Id { get; init; }
    public UserOption UserId { get; init; }
    public UserOrganizationOption UserOrganizationId { get; init; }
    public UserRoleOption UserRoleId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserOrganizationRole, UserOrganizationRoleVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserOrganizationRoleId))
               .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.User))
               .ForMember(d => d.UserOrganizationId, opt => opt.MapFrom(s => s.UserOrganization))
               .ForMember(d => d.UserRoleId, opt => opt.MapFrom(s => s.UserRole));
    }
}
