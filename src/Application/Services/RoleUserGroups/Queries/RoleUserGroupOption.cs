namespace Engage.Application.Services.RoleUserGroups.Queries;

public class RoleUserGroupOption : IMapFrom<RoleUserGroup>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RoleUserGroup, RoleUserGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.RoleUserGroupId));
    }
}