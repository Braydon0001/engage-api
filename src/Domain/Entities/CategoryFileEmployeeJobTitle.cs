namespace Engage.Domain.Entities;

public class CategoryFileEmployeeJobTitle : CategoryFileTarget
{
    public int EmployeeJobTitleId { get; set; }

    public EmployeeJobTitle EmployeeJobTitle { get; set; }
}
