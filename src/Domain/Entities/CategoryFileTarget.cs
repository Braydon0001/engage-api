namespace Engage.Domain.Entities
{
    public class CategoryFileTarget : BaseAuditableEntity
    {
        public int CategoryFileTargetId { get; set; }
        public int CategoryFileId { get; set; }

        // Navigation Properties

        public CategoryFile CategoryFile { get; set; }

    }
}
