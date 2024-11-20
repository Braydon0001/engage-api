namespace Engage.Domain.Entities
{
    public class EmployeeType : BaseAuditableEntity
    {
        public int EmployeeTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
