using System.ComponentModel;

namespace Engage.Domain.Enums
{
    public enum LeaveEntryStatus
    {
        Created,
        Submitted,
        [Description("Rejected From Adjustment")]
        RejectedFromAdjustment,
        Rejected,
        Approved
    }
}
