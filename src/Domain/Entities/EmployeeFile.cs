namespace Engage.Domain.Entities
{
    public class EmployeeFile : BaseAuditableEntity
    {
        public int EmployeeFileId { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeFileTypeId { get; set; }

        public List<JsonFile> Files { get; set; }
        public Employee Employee { get; set; }
        public EmployeeFileType EmployeeFileType { get; set; }
    }
}
