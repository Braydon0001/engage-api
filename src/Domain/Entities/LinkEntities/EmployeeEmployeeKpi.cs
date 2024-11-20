using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engage.Domain.Entities.LinkEntities
{
    public class EmployeeEmployeeKpi
    {
        public EmployeeEmployeeKpi(int employeeId, int employeeKpiId, int score, int? employeeKpiTierId)
        {
            EmployeeId = employeeId;
            EmployeeKpiId = employeeKpiId;
            Score = score;
            EmployeeKpiTierId = employeeKpiTierId;
        }

        public int EmployeeId { get; set; }
        public int EmployeeKpiId { get; set; }
        public int? EmployeeKpiTierId { get; set; }
        public int Score { get; set; }

        public Employee Employee { get; set; }
        public EmployeeKpi EmployeeKpi { get; set; }
        public EmployeeKpiTier EmployeeKpiTier { get; set; }
    }
}
