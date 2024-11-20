// auto-generated
namespace Engage.Domain.Entities;

public class WebPageEmployee : BaseAuditableEntity
{
    public int WebPageEmployeeId { get; set; }
    public int EmployeeId { get; set; }
    public int WebPageId { get; set; }
    public DateTime ViewDate { get; set; }

    // Navigation Properties

    public Employee Employee { get; set; }
    public WebPage WebPage { get; set; }
}