namespace Engage.Domain.Entities;

public class EmployeeAsset : BaseAuditableEntity
{
    public EmployeeAsset()
    {
        EmployeeAssetHistories = new HashSet<EmployeeAssetHistory>();
    }
    public int EmployeeAssetId { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeeAssetTypeId { get; set; }
    public int EmployeeAssetBrandId { get; set; }
    public int AssetStatusId { get; set; }
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

    // Navigation Properties
    public Employee Employee { get; set; }
    public EmployeeAssetType EmployeeAssetType { get; set; }
    public EmployeeAssetBrand EmployeeAssetBrand { get; set; }
    public AssetStatus AssetStatus { get; set; }
    public ICollection<EmployeeAssetHistory> EmployeeAssetHistories { get; set; }
}

