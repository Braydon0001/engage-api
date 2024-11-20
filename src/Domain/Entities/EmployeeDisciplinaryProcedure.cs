namespace Engage.Domain.Entities;

public class EmployeeDisciplinaryProcedure : BaseAuditableEntity
{
    public int EmployeeDisciplinaryProcedureId { get; set; }
    public int EmployeeId { get; set; }
    public string Description { get; set; }
    public DateTime DisciplinaryProcedureDate { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Propterties
    public Employee Employee { get; set; }
}
