namespace Engage.Application.Services.SecurityOrganizations.Queries;

public class SecurityOrganizationVm : IMapFrom<SecurityOrganization>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string ExternalId { get; set; }
    public OptionDto OwnerId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityOrganization, SecurityOrganizationVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SecurityOrganizationId))
               .ForMember(d => d.OwnerId, opt => opt.Ignore());
    }
}
