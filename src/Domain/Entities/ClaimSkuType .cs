using Engage.Domain.Common;

namespace Engage.Domain.Entities
{
    public class ClaimSkuType : BaseAuditableEntity
    {
        //Required
        public int ClaimSkuTypeId { get; set; }
        public string Name { get; set; }
        public bool IsVatInclusive { get; set; }
    }
}
