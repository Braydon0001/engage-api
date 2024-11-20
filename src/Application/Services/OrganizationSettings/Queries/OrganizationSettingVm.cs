
namespace Engage.Application.Services.OrganizationSettings.Queries;

public class OrganizationSettingVm : IMapFrom<OrganizationSetting>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string FaviconUrl { get; set; }
    public string LogoUrl { get; set; }
    public string LogoDarkUrl { get; set; }
    public JsonThemeSetting OrganizationTheme { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrganizationSetting, OrganizationSettingVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrganizationSettingId));
    }
}
