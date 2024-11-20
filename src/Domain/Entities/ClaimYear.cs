using Engage.Domain.Common;
using System.Collections.Generic;

namespace Engage.Domain.Entities
{
    public class ClaimYear : BaseAuditableEntity
    {
        public ClaimYear()
        {
            ClaimPeriods = new HashSet<ClaimPeriod>();
        }
        //Required
        public int ClaimYearId { get; set; }
        public string Name { get; set; }

        public ICollection<ClaimPeriod> ClaimPeriods { get; set; }
    }
}
