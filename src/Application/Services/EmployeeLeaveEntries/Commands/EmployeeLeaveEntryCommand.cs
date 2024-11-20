namespace Engage.Application.Services.EmployeeLeaveEntries.Commands;

public class EmployeeLeaveEntryCommand : IMapTo<EmployeeLeaveEntry>
{
    public int LeaveTypeId { get; set; }
    public DateTime FromDate { get; set; }
    public bool FromHalfDay { get; set; }
    public DateTime ToDate { get; set; }
    public bool ToHalfDay { get; set; }
    public LeaveEntryStatus Status { get; set; }
    public string Comment { get; set; }
    public bool AdjustLeave { get; set; }
    public string ManagerComment { get; set; }
    public bool Processed { get; set; }
    public DateTime ProcessedDate { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<EmployeeLeaveEntryCommand, EmployeeLeaveEntry>();
    }
}
