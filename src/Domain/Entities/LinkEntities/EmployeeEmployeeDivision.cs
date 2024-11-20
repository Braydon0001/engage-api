namespace Engage.Domain.Entities.LinkEntities
{
    public class EmployeeEmployeeDivision
    {
        public int EmployeeId { get; set; }
        public int EmployeeDivisionId { get; set; }

        public Employee Employee { get; set; }
        public EmployeeDivision EmployeeDivision { get; set; }
    }
}
