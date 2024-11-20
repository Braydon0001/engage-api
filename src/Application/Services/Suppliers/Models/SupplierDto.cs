namespace Engage.Application.Services.Suppliers.Models;

public class SupplierDto : IMapFrom<Supplier>
{
    public int Id { get; set; }
    public int StakeholderId { get; set; }
    public int SupplierGroupId { get; set; }
    public int? PrimaryLocationId { get; set; }
    public int? PrimaryContactId { get; set; }
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
    public int? ClaimAccountManagerId { get; set; }
    public int? ClaimManagerId { get; set; }
    public string ClaimReportTitle { get; set; }
    public string ClaimReportAccountNumber { get; set; }
    public bool IsClaimAccountManagerRequired { get; set; }
    public bool IsClaimFloatRequired { get; set; }

    public string ThemeColor { get; init; }
    public string ThemeCustomColor { get; init; }
    public JsonThemeSetting JsonTheme { get; init; }

    public bool Disabled { get; set; }
    public ICollection<OptionDto> SupplierTypes { get; set; }
    public ICollection<OptionDto> EngageBrands { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<Supplier, SupplierDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierId))
            .ForMember(d => d.SupplierTypes, opt => opt.MapFrom(s => s.SupplierSupplierTypes
                                                                         .Select(s => s.SupplierType)
                                                                         .Select(s => new OptionDto() { Id = s.Id, Name = s.Name })))
            .ForMember(d => d.EngageBrands, opt => opt.MapFrom(s => s.SupplierEngageBrands
                                                                         .Select(s => s.EngageBrand)
                                                                         .Select(s => new OptionDto() { Id = s.Id, Name = s.Name })));
    }
}
