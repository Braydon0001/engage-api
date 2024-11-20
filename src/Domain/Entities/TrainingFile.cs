namespace Engage.Domain.Entities
{
    public class TrainingFile : BaseAuditableEntity
    {
        public int TrainingFileId { get; set; }
        public int TrainingId { get; set; }
        public int TrainingFileTypeId { get; set; }
        public List<JsonFile> Files { get; set; }

        public Training Training { get; set; }
        public TrainingFileType TrainingFileType { get; set; }
    }
}
