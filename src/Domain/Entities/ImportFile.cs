namespace Engage.Domain.Entities
{
    public class ImportFile : BaseAuditableEntity
    {
        public int ImportFileId { get; set; }
        public string FileName { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
    }
}
