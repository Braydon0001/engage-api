namespace Engage.Domain.Entities;

public class CategoryFile : BaseAuditableEntity
{

    public int CategoryFileId { get; set; }
    public int CategoryFileTypeId { get; set; }
    public int? StoreId { get; set; }
    public int? CategoryGroupId { get; set; }
    public int? CategorySubGroupId { get; set; }
    public string Name { get; set; }
    public string Note { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<JsonFile> Files { get; set; }
    public JsonRule TargetRule { get; set; }

    // Navigation Properties

    public CategoryFileType CategoryFileType { get; set; }
    public Store Store { get; set; }
    public CategoryGroup CategoryGroup { get; set; }
    public CategorySubGroup CategorySubGroup { get; set; }


}