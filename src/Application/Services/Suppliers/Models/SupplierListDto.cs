﻿namespace Engage.Application.Services.Suppliers.Models;

public class SupplierListDto : IMapFrom<Supplier>
{
    public int Id { get; set; }
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
    public bool Disabled { get; set; }

    public string ThemeColor { get; init; }
    public string ThemeCustomColor { get; init; }
    public JsonThemeSetting JsonTheme { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Supplier, SupplierListDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.SupplierId));
    }
}