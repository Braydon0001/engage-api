namespace Engage.Domain.Entities;

public class EmployeeTransactionType : BaseAuditableEntity
{
    public int EmployeeTransactionTypeId { get; set; }
    public int? EmployeeTransactionTypeGroupId { get; set; }
    public string Name { get; set; }
    public bool IsPositive { get; set; }
    public bool IsRecurring { get; set; }
    public List<JsonEmployeeField> Fields { get; set; }
    public float? OvertimeMultiple { get; set; }

    public EmployeeTransactionTypeGroup EmployeeTransactionTypeGroup { get; set; }
}