namespace Engage.Application.Services.SecurityOrganizations.Queries;

public class SecurityOrganizationOption : IMapFrom<SecurityOrganization>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityOrganization, SecurityOrganizationOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SecurityOrganizationId));
    }
}