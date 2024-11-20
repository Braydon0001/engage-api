namespace Engage.Application.Services.ClaimReports.Models;

public class ReportListVM<T>
{
    public ReportListVM()
    {
        ReportSubTotals = new List<ReportSubTotal>();
    }
    public object Count { get; set; }
    public object ReportName { get; set; }
    public object ReportTitle { get; set; }
    public object ReportAccountNumber { get; set; }
    public List<T> Data { get; set; }
    public IEnumerable<object> CleanData { get; set; }
    public List<ReportSubTotal> ReportSubTotals { get; set; }
    public bool IsPaid { get; set; } = false;
    public List<string> ColumnNames { get; set; }
    public string ClassificationName { get; set; }
    public string RegionName { get; set; }
    public string SupplierName { get; set; }
}

public class ReportSubTotal
{
    public string Name { get; set; }
    public decimal Value { get; set; }
}
