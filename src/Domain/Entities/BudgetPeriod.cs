using Engage.Domain.Common;
using System;
using System.Collections.Generic;

namespace Engage.Domain.Entities
{
    public class BudgetPeriod : BaseAuditableEntity
    {
        public int BudgetPeriodId { get; set; }
        public int BudgetYearId { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public BudgetPeriod()
        {
            Budgets = new HashSet<Budget>();
        }

        // Navigation Properties
        public BudgetYear BudgetYear { get; set; }        
        public ICollection<Budget> Budgets { get; private set; }
    }
}
