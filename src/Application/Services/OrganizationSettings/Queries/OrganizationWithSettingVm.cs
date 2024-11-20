namespace Engage.Application.Services.OrganizationSettings.Queries;

public class OrganizationWithSettingVm : IMapFrom<Organization>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string TenantIdentifier { get; init; }
    public List<JsonSetting> Settings { get; init; }
    public OrganizationSettingVm OrganizationSetting { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Organization, OrganizationWithSettingVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrganizationId));
    }
}

