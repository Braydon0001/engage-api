namespace Engage.Application.Services.SecurityOrganizations.Queries;

public class SecurityOrganizationDto : IMapFrom<SecurityOrganization>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Slug { get; init; }
    public int OwnerId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityOrganization, SecurityOrganizationDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SecurityOrganizationId));
    }
}
