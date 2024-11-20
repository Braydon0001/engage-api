using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engage.Domain.Entities.LinkEntities
{
    public class EmployeeStoreKpi
    {
        public EmployeeStoreKpi(int employeeId, int employeeKpiId, int storeId, int? employeeKpiTierId)
        {
            EmployeeId = employeeId;
            EmployeeKpiId = employeeKpiId;
            StoreId = storeId;
            EmployeeKpiTierId = employeeKpiTierId;
        }

        public int EmployeeId { get; set; }
        public int StoreId { get; set; }
        public int EmployeeKpiId { get; set; }
        public int? EmployeeKpiTierId { get; set; }
        public int Score { get; set; }

        public Employee Employee { get; set; }
        public Store Store { get; set; }
        public EmployeeKpi EmployeeKpi { get; set; }
        public EmployeeKpiTier EmployeeKpiTier { get; set; }
    }
}
