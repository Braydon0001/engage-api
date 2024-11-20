using System.ComponentModel;

namespace Engage.Domain.Enums
{
    public enum ProgressStatus
    {
        Planned,
        Booked,
        Started,
        [Description("In Progress")]
        InProgress,
        Completed
    }
}
