namespace Engage.Domain.Common;

public class BaseContactEntity : BaseAuditableEntity
{
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string MiddleName { get; set; }
    public string MobilePhone { get; set; }
    public string Description { get; set; }
}
