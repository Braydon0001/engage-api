namespace Engage.Domain.Entities;

public class ExpenseType : BaseAuditableEntity
{
    public int ExpenseTypeId { get; set; }
    public string Name { get; set; }
}