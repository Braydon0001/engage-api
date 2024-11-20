using Engage.Domain.Common;

namespace Engage.Domain.Entities.LinkEntities
{
    public class BudgetYearVersion : BaseAuditableEntity
    {
        public int BudgetYearId { get; set; }
        public int BudgetVersionId { get; set; }

        // Navigation Properties
        public BudgetYear BudgetYear { get; set; }
        public BudgetVersion BudgetVersion { get; set; }
    }
}
