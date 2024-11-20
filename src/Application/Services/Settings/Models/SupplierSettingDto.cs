namespace Engage.Application.Services.Settings.Models;

public class SupplierSettingDto : SettingDto, IMapFrom<SupplierSetting>
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSetting, SupplierSettingDto>()
            .ForMember(d => d.SupplierName, opts => opts.MapFrom(s => s.Supplier.Name))
            .ForMember(d => d.SettingName, opts => opts.MapFrom(s => s.Setting.Name))
            .ForMember(d => d.NormalizedSettingName, opts => opts.MapFrom(s => s.Setting.Name.ToLower()));
    }
}
