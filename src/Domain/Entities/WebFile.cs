// auto-generated

namespace Engage.Domain.Entities;

public class WebFile : BaseAuditableEntity
{
    public int WebFileId { get; set; }
    public int WebFileCategoryId { get; set; }
    public int FileTypeId { get; set; }
    public int TargetStrategyId { get; set; }
    public int? EmployeeId { get; set; }
    public int? StoreId { get; set; }
    public int? NPrintingId { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public WebFileCategory WebFileCategory { get; set; }
    public FileType FileType { get; set; }
    public TargetStrategy TargetStrategy { get; set; }
    public Employee Employee { get; set; }
    public Store Store { get; set; }
    public NPrinting NPrinting { get; set; }
}