namespace Engage.Domain.Entities.LinkEntities
{
    public class EmployeeManager
    {
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }

        public Employee Employee { get; set; }
        public Employee Manager { get; set; }
    }
}
