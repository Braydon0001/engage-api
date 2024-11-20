namespace Engage.Domain.Entities;

public class EmployeeTransactionRemunerationType : BaseAuditableEntity
{
    public int EmployeeTransactionRemunerationTypeId { get; set; }
    public string Name { get; set; }
}