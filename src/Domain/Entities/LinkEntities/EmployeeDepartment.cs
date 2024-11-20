namespace Engage.Domain.Entities.LinkEntities
{
    public class EmployeeDepartment
    {
        public int EmployeeId { get; set; }
        public int EngageDepartmentId { get; set; }

        public Employee Employee { get; set; }
        public EngageDepartment EngageDepartment { get; set; }
    }
}
