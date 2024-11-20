using Engage.Domain.Common;
using System;

namespace Engage.Domain.Entities
{
    public class ClaimPeriod : BaseAuditableEntity
    {
        public int ClaimPeriodId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClaimYearId { get; set; }

        //Navigation Props
        public ClaimYear ClaimYear { get; set; }
    }
}
