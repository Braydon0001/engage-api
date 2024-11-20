namespace Engage.Application.Services.EmployeeAssets.Commands;

public class EmployeeAssetCommand
{
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
}
