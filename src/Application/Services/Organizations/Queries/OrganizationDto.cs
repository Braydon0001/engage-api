namespace Engage.Application.Services.Organizations.Queries;

public class OrganizationDto : IMapFrom<Organization>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string TenantIdentifier { get; init; }
    public List<JsonSetting> Settings { get; init; }

    public string ThemeColor { get; init; }
    public string ThemeCustomColor { get; init; }
    public JsonThemeSetting JsonTheme { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Organization, OrganizationDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrganizationId));
    }
}
