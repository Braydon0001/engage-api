namespace Engage.Domain.Entities;

public class EmployeeWorkRoleContact : BaseContactEntity
{
    public int EmployeeWorkRoleContactId { get; set; }
    public int EmployeeWorkRoleId { get; set; }
    public string Title { get; set; }

    public EmployeeWorkRole EmployeeWorkRole { get; set; }
}
