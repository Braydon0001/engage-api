namespace Engage.Application.Services.Organizations.Queries;

public class OrganizationOption : IMapFrom<Organization>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Organization, OrganizationOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrganizationId));
    }
}