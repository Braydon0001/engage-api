// auto-generated
namespace Engage.Domain.Entities;

public class PayrollPeriod : BaseAuditableEntity
{
    public int PayrollPeriodId { get; set; }
    public int PayrollYearId { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Navigation Properties

    public PayrollYear PayrollYear { get; set; }
}