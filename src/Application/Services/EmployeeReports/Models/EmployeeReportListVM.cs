namespace Engage.Application.Services.EmployeeReports.Models;

public class EmployeeReportListVM<T>
{
    public object Count { get; set; }
    public object ReportName { get; set; }
    public List<T> Data { get; set; }
    public List<string> ColumnNames { get; set; }
}
