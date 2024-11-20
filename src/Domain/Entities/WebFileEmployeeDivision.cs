namespace Engage.Domain.Entities;

public class WebFileEmployeeDivision : WebFileTarget
{
    public int EmployeeDivisionId { get; set; }

    // Navigation Properties

    public EmployeeDivision EmployeeDivision { get; set; }
}
