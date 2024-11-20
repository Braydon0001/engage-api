using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engage.Domain.Entities
{
    public class EmployeeKpiTier : BaseAuditableEntity
    {
        public EmployeeKpiTier()
        {
            EmployeeKpis = new HashSet<EmployeeEmployeeKpi>();
            EmployeeStoreKpis = new HashSet<EmployeeStoreKpi>();
        }
        public int EmployeeKpiTierId { get; set; }
        public int EmployeeKpiId { get; set; }
        public string Name { get; set; }
        public int No { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }

        public EmployeeKpi EmployeeKpi { get; set; }
        public ICollection<EmployeeEmployeeKpi> EmployeeKpis { get; private set; }
        public ICollection<EmployeeStoreKpi> EmployeeStoreKpis { get; private set; }
    }
}
