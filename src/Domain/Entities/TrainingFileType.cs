namespace Engage.Domain.Entities
{
    public class TrainingFileType : BaseAuditableEntity
    {
        public int TrainingFileTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
