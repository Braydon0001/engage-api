namespace Engage.Application.Models.Configuration
{
    public class ClaimSettings
    {
        public int FNBReportTypeId { get; set; }
        public string DefaultStoreClaimPaymentEmailAddress { get; set; }
        public int ClaimVoucherTypeId { get; set; }
        public string ExcludeCreatedBy { get; set; }
    }
}
