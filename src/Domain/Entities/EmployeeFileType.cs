namespace Engage.Domain.Entities
{
    public class EmployeeFileType : BaseAuditableEntity
    {
        public int EmployeeFileTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
