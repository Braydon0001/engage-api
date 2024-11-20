using Engage.Domain.Common;
using Engage.Domain.Entities.LinkEntities;
using System.Collections.Generic;

namespace Engage.Domain.Entities
{
    public class BudgetYear : BaseAuditableEntity
    {
        public int BudgetYearId { get; set; }
        public string Name { get; set; }

        public BudgetYear()
        {
            Budgets = new HashSet<Budget>();
            BudgetYearVersions = new HashSet<BudgetYearVersion>();
            BudgetPeriods = new HashSet<BudgetPeriod>();
        }

        // Navigation Properties
        public ICollection<BudgetPeriod> BudgetPeriods { get; set; }
        public ICollection<BudgetYearVersion> BudgetYearVersions { get; set; }
        public ICollection<Budget> Budgets { get; set; }
    }
}
