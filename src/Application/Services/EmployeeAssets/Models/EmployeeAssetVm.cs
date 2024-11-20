namespace Engage.Application.Services.EmployeeAssets.Models;

public class EmployeeAssetVm : IMapFrom<EmployeeAsset>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto EmployeeAssetTypeId { get; set; }
    public OptionDto EmployeeAssetBrandId { get; set; }
    public OptionDto AssetStatusId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Contract { get; set; }
    public string MobileNumber { get; set; }
    public string Sim { get; set; }
    public string Imei { get; set; }
    public string SerialNumber { get; set; }
    public string Note { get; set; }
    public DateTime? RecievedDate { get; set; }
    public DateTime? HandedBackDate { get; set; }
    public List<JsonFile> FileMerchandiserAppUsage { get; set; }
    public List<JsonFile> FileDeviceUsage { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeAsset, EmployeeAssetVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeAssetId))
            .ForMember(d => d.FileMerchandiserAppUsage, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "merchandiserappusage")))
            .ForMember(d => d.FileDeviceUsage, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "deviceusage")))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, $"{s.Employee.FirstName} {s.Employee.LastName}")))
            .ForMember(d => d.EmployeeAssetTypeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeAssetTypeId, s.EmployeeAssetType.Name)))
            .ForMember(d => d.EmployeeAssetBrandId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeAssetBrandId, s.EmployeeAssetBrand.Name)))
            .ForMember(d => d.AssetStatusId, opt => opt.MapFrom(s => new OptionDto(s.AssetStatusId, s.AssetStatus.Name)));
    }
}
