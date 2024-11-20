namespace Engage.Application.Services.EmployeeStoreKpis.Models;
public class EmployeeStoreKpiDto : IMapFrom<EmployeeStoreKpi>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public int EmployeeKpiId { get; set; }
    public string EmployeeKpiName { get; set; }
    public int? EmployeeKpiTierId { get; set; }
    public string EmployeeKpiTierName { get; set; }
    public int Score { get; set; }
}
