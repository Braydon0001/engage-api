namespace Engage.Domain.Entities;

public class CategoryFileEmployee : CategoryFileTarget
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}
