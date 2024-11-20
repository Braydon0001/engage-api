namespace Engage.Domain.Entities.LinkEntities;

public class EmployeeStoreArchive : BaseAuditableEntity
{
    public int EmployeeStoreArchiveId { get; set; }
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public int? EngageDepartmentCategoryId { get; set; }
    public int EngageSubGroupId { get; set; }
    public int FrequencyTypeId { get; set; }
    public int Frequency { get; set; }
    public string Note { get; set; }

    // Navigation Properties
    public Employee Employee { get; set; }
    public Store Store { get; set; }
    public EngageDepartmentCategory EngageDepartmentCategory { get; set; }
    public EngageSubGroup EngageSubGroup { get; set; }
    public FrequencyType GetFrequencyType { get; set; }
}
