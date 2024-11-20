using Engage.Domain.Common;
using Engage.Domain.Entities.LinkEntities;
using System;
using System.Collections.Generic;

namespace Engage.Domain.Entities
{
    public class Promotion : BaseAuditableEntity
    {
        public Promotion()
        {
            PromotionStores = new HashSet<PromotionStore>();
        }

        public int PromotionId { get; set; }
        public int PromotionTypeId { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime OrderStartDate { get; set; }
        public DateTime? OrderEndDate { get; set; }
        public decimal Amount { get; set; }

        // Navigation Properties

        public PromotionType PromotionType { get; set; }

        public ICollection<PromotionStore> PromotionStores { get; set; }
    }
}
