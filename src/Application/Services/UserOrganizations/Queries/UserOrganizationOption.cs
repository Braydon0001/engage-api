namespace Engage.Application.Services.UserOrganizations.Queries;

public class UserOrganizationOption : IMapFrom<UserOrganization>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserOrganization, UserOrganizationOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserOrganizationId));
    }
}