using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engage.Domain.Entities
{
    public class EmployeeBadge : BaseAuditableEntity
    {
        public EmployeeBadge()
        {
            EmployeeBadges = new HashSet<EmployeeEmployeeBadge>();
        }
        public int EmployeeBadgeId { get; set; }
        public int EmployeeBadgeTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }

        public EmployeeBadgeType EmployeeBadgeType { get; set; }
        public ICollection<EmployeeEmployeeBadge> EmployeeBadges { get; private set; }
    }
}
