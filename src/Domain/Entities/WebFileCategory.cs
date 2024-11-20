// auto-generated
namespace Engage.Domain.Entities;

public class WebFileCategory : BaseAuditableEntity
{
    public WebFileCategory()
    {
        WebFiles = new HashSet<WebFile>();
    }

    public int WebFileCategoryId { get; set; }
    public int WebFileGroupId { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public int Order { get; set; }

    // Navigation Properties

    public WebFileGroup WebFileGroup { get; set; }

    public ICollection<WebFile> WebFiles { get; set; }
}