namespace Engage.Application.Services.EmployeeTransactionReports.Commands
{
    public class EmployeeTransactionReportCommand
    {
        public List<int> EngageRegionIds { get; set; }
        public int? EmployeeTransactionTypeId { get; set; }
        public int PayrollPeriodId { get; set; }
    }
}
