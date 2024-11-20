namespace Engage.Application.Services.Settings.Models;

public class TenantSettingDto : SettingDto, IMapFrom<TenantSetting>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TenantSetting, TenantSettingDto>()
            .ForMember(d => d.SettingName, opts => opts.MapFrom(s => s.Setting.Name))
            .ForMember(d => d.NormalizedSettingName, opts => opts.MapFrom(s => s.Setting.Name.ToLower()));
    }
}
