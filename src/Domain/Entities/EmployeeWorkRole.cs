namespace Engage.Domain.Entities;

public class EmployeeWorkRole : BaseAuditableEntity
{
    public EmployeeWorkRole()
    {
        EmployeeWorkRoleContacts = new HashSet<EmployeeWorkRoleContact>();
    }
    public int EmployeeWorkRoleId { get; set; }
    public int EmployeeId { get; set; }
    public int ManagerId { get; set; }
    public int EmploymentTypeId { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string ExitReason { get; set; }
    public string Note { get; set; }
    public int GradeLevel { get; set; }
    public int? GradeId { get; set; }
    public int? VacancyId { get; set; }
    public int? StatusId { get; set; }
    public bool IsPromotion { get; set; }
    public bool IsCurrentRole { get; set; }

    // Navigation Properties
    public Employee Employee { get; set; }
    public Employee Manager { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public WorkRoleStatus Status { get; set; }
    public Grade Grade { get; set; }
    public Vacancy Vacancy { get; set; }
    public ICollection<EmployeeWorkRoleContact> EmployeeWorkRoleContacts { get; set; }
}
