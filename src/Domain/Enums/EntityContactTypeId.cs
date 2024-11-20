using System.ComponentModel;

namespace Engage.Domain.Enums
{
    public enum EntityContactTypeId
    {
        Default = 1,
        [Description("Claim Account Manager")]
        ClaimAccountManager,
        [Description("Claim Notifier")]
        ClaimManager,
        [Description("Claim Payment Notifier")]
        ClaimPaymentNotifier,        
    }
}
