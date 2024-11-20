namespace Engage.Domain.Entities.LinkEntities
{
    public class StoreStoreDepartment
    {
        public int StoreId { get; set; }
        public int StoreDepartmentId { get; set; }

        // Navigation Properties
        public Store Store { get; set; }
        public StoreDepartment StoreDepartment { get; set; }
    }
}
