namespace Engage.Domain.Entities;

public class EmployeeTransactionTypeGroup : BaseAuditableEntity
{
    public int EmployeeTransactionTypeGroupId { get; set; }
    public string Name { get; set; }
}