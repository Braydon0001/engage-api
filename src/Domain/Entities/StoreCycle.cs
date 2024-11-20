namespace Engage.Domain.Entities
{
    public class StoreCycle : BaseAuditableEntity
    {
        public int StoreCycleId { get; set; }
        public int StoreId { get; set; }
        public int EngageDepartmentId { get; set; }
        public int StoreCycleOperationId { get; set; }
        public int FrequencyTypeId { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public string Note { get; set; }

        // Navigation Properties
        public Store Store { get; set; }
        public EngageDepartment EngageDepartment { get; set; }
        public StoreCycleOperation StoreCycleOperation { get; set; }
        public FrequencyType FrequencyType { get; set; }
    }
}
