namespace Engage.Application.Services.Suppliers.Models;

public class SupplierVm : IMapFrom<Supplier>
{
    public int Id { get; set; }
    public OptionDto SupplierGroupId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string VATNumber { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<JsonSetting> Settings { get; set; }
    public bool OrderModuleEnabled { get; set; }
    public bool IsSupplierProductsOnly { get; set; }
    public bool ClaimModuleEnabled { get; set; }
    public bool IsDairy { get; set; }
    public bool IsEmployeeClaim { get; set; }
    public bool IsClaimAccountManager { get; set; }
    public bool IsClaimManager { get; set; }
    public string ClaimReportTitle { get; set; }
    public string ClaimReportAccountNumber { get; set; }
    public bool Disabled { get; set; }
    public bool IsClaimAccountManagerRequired { get; set; }
    public bool IsClaimFloatRequired { get; set; }

    public string ThemeColor { get; init; }
    public string ThemeCustomColor { get; init; }
    public JsonThemeSetting JsonTheme { get; init; }

    public List<OptionDto> SupplierTypeIds { get; set; }
    public List<OptionDto> EngageBrandIds { get; set; }
    public List<JsonFile> FileIcon { get; set; }
    public List<JsonFile> StringSettings { get; set; }
    public List<JsonFile> NumberSettings { get; set; }
    public List<JsonFile> BooleanSettings { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Supplier, SupplierVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierId))
            .ForMember(d => d.SupplierGroupId, opt => opt.MapFrom(s => new OptionDto(s.SupplierGroupId, s.SupplierGroup.Name)))
            .ForMember(d => d.SupplierTypeIds, opt => opt.MapFrom(s => s.SupplierSupplierTypes.Select(o => new OptionDto() { Id = o.SupplierTypeId, Name = o.SupplierType.Name })))
            .ForMember(d => d.EngageBrandIds, opt => opt.MapFrom(s => s.SupplierEngageBrands.Select(o => new OptionDto() { Id = o.EngageBrandId, Name = o.EngageBrand.Name })))
            .ForMember(d => d.FileIcon, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "icon")));
    }
}
