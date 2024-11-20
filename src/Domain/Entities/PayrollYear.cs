// auto-generated
namespace Engage.Domain.Entities;

public class PayrollYear : BaseAuditableEntity
{
    public int PayrollYearId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}