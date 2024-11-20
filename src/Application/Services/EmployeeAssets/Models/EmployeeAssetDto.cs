namespace Engage.Application.Services.EmployeeAssets.Models;

public class EmployeeAssetDto : IMapFrom<EmployeeAsset>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public DateTime? EmployeeEndDate { get; set; }
    public int EmployeeAssetTypeId { get; set; }
    public string EmployeeAssetTypeName { get; set; }
    public int EmployeeAssetBrandId { get; set; }
    public string EmployeeAssetBrandName { get; set; }
    public int AssetStatusId { get; set; }
    public string AssetStatusName { get; set; }
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
    public List<JsonFile> Files { get; set; }
    public List<EmployeeAssetHistoryDto> EmployeeAssetHistories { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeAsset, EmployeeAssetDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeAssetId))
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => $"{s.Employee.FirstName} {s.Employee.LastName}"))
            .ForMember(d => d.EmployeeAssetHistories,
                    opt => opt.MapFrom(s => s.EmployeeAssetHistories
                        .OrderByDescending(e => e.EmployeeAssetHistoryId)
                        .Select(e => new EmployeeAssetHistoryDto()
                        {
                            Id = e.EmployeeAssetHistoryId,
                            EmployeeCode = e.OldEmployee.Code,
                            EmployeeId = e.OldEmployeeId,
                            EmployeeName = $"{e.OldEmployee.FirstName} {e.OldEmployee.LastName}",
                            EmployeeAssetId = e.EmployeeAssetId,
                            CreatedDate = e.Created,
                        })
                        .ToList()));
    }
}

public class EmployeeAssetHistoryDto
{
    public int Id { get; set; }
    public int EmployeeAssetId { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public DateTime? CreatedDate { get; set; }
}
