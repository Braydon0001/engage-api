namespace Engage.Domain.Entities;

public class EmployeePopiConsent : BaseAuditableEntity
{
    public int EmployeePopiConsentId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime DateOfConsent { get; set; }

    //Navigation Properties
    public Employee Employee { get; set; }
}
