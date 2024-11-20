// auto-generated
namespace Engage.Domain.Entities;

public class ProductYear : BaseAuditableEntity
{
    public int ProductYearId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}