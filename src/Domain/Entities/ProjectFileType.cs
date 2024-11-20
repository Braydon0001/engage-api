namespace Engage.Domain.Entities
{
    public class ProjectFileType : BaseAuditableEntity
    {
        public int ProjectFileTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
