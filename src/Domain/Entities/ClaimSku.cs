using Engage.Domain.Common;
using System;

namespace Engage.Domain.Entities
{
    public class ClaimSku : BaseAuditableEntity
    {
        public int ClaimSkuId { get; set; }
        public int ClaimId { get; set; }
        public int ClaimSkuTypeId { get; set; }
        public int ClaimSkuStatusId { get; set; }
        public int ClaimQuantityTypeId { get; set; }
        public decimal Amount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public int DCProductId { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string Note { get; set; }
                
        //Navigation Props
        public Claim Claim { get; set; }
        public ClaimSkuType ClaimSkuType { get; set; }
        public ClaimSkuStatus ClaimSkuStatus { get; set; }
        public ClaimQuantityType ClaimQuantityType { get; set; }
        public DCProduct DCProduct { get; set; }
    }
}
