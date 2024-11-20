// auto-generated
namespace Engage.Domain.Entities;

public class WebFileTarget : BaseAuditableEntity
{
    public int WebFileTargetId { get; set; }
    public int WebFileId { get; set; }

    // Navigation Properties

    public WebFile WebFile { get; set; }
}