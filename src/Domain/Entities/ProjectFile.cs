namespace Engage.Domain.Entities
{
    public class ProjectFile : BaseAuditableEntity
    {
        public int ProjectFileId { get; set; }
        public int ProjectId { get; set; }
        public int ProjectFileTypeId { get; set; }
        public List<JsonFile> Files { get; set; }

        public Project Project { get; set; }
        public ProjectFileType ProjectFileType { get; set; }
    }
}
