using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engage.Domain.Entities.LinkEntities
{
    public class EmployeeEmployeeBadge
    {
        public EmployeeEmployeeBadge(int employeeId, int employeeBadgeId)
        {
            EmployeeId = employeeId;
            EmployeeBadgeId = employeeBadgeId;
        }

        public int EmployeeId { get; set; }
        public int EmployeeBadgeId { get; set; }

        public Employee Employee { get; set; }
        public EmployeeBadge EmployeeBadge { get; set; }
    }
}
