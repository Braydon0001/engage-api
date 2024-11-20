namespace Engage.Domain.Entities.LinkEntities
{
    public class OrderEngageDepartment
    {
        public int OrderId { get; set; }
        public int EngageDepartmentId { get; set; }

        // Navigation Properties
        public Order Order { get; set; }
        public EngageDepartment EngageDepartment { get; set; }

    }
}
