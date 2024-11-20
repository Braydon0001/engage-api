using Engage.Domain.Common;
using Engage.Domain.Entities.LinkEntities;
using System.Collections.Generic;

namespace Engage.Domain.Entities
{
    public class Report : BaseAuditableEntity
    {
        public Report()
        {
            EmployeeReports = new HashSet<EmployeeReport>();
        }
        public int ReportId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation Properties
        public ICollection<EmployeeReport> EmployeeReports { get; private set; }
    }
}
