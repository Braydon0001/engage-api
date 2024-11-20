using Engage.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engage.Domain.Entities
{
    public class StatsStoresByRegion
    {
        public int StatsStoresByRegionId { get; set; }
        public int EngageRegionId { get; set; }
        public int Stores { get; set; }
        public DateTime CreatedDate { get; set; }

        public EngageRegion EngageRegion { get; set; }
    }
}
