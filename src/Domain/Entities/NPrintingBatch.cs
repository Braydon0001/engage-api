// auto-generated

namespace Engage.Domain.Entities;

public class NPrintingBatch : BaseAuditableEntity
{
    public int NPrintingBatchId { get; set; }
    public int WebFileCategoryId { get; set; }
    public int FileTypeId { get; set; }
    public string Directory { get; set; }
    public string Report { get; set; }
    public string DisplayName { get; set; }

    // Navigation Properties

    public WebFileCategory WebFileCategory { get; set; }
    public FileType FileType { get; set; }
    public ICollection<NPrinting> NPrintings { get; set; } = new HashSet<NPrinting>();
}