using Engage.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engage.Domain.Entities
{
    public class StatsOrdersByRegion
    {
        public int StatsOrdersByRegionId { get; set; }
        public int EngageRegionId { get; set; }
        public int OrdersLast1Day { get; set; }
        public int OrdersLast7Days { get; set; }
        public int OrdersAll { get; set; }
        public DateTime CreatedDate { get; set; }

        public EngageRegion EngageRegion { get; set; }
    }
}
