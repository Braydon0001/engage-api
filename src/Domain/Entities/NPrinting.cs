// auto-generated
namespace Engage.Domain.Entities;

public class NPrinting : BaseAuditableEntity
{
    public int NPrintingId { get; set; }
    public int NPrintingBatchId { get; set; }
    public string FileName { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string Error { get; set; }

    // Navigation Properties

    public NPrintingBatch NPrintingBatch { get; set; }
}