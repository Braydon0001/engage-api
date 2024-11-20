namespace Engage.Application.Services.EmployeeLeaveEntries.Models;

public class EmployeeLeaveEntryDto : IMapFrom<EmployeeLeaveEntry>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int LeaveTypeId { get; set; }
    public string LeaveTypeName { get; set; }
    public LeaveEntryStatus Status { get; set; }
    public string StatusName { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string Date { get; set; }
    public bool FromHalfDay { get; set; }
    public bool ToHalfDay { get; set; }
    public string Comment { get; set; }
    public bool AdjustLeave { get; set; }
    public string ManagerComment { get; set; }
    public bool Processed { get; set; }
    public DateTime ProcessedDate { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeLeaveEntry, EmployeeLeaveEntryDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.EmployeeLeaveEntryId))
            .ForMember(e => e.LeaveTypeName, opt => opt.MapFrom(d => d.LeaveType.Name))
            .ForMember(e => e.Status, opt => opt.MapFrom(d => d.Status))
            .ForMember(e => e.StatusName, opt => opt.MapFrom(d => GetStatusValue(d.Status)))
            .ForMember(e => e.Date, opt => opt.MapFrom(d => DateUtils.ShortDateString(d.FromDate, d.ToDate)));
    }

    // Use a static mathod to avoid a potential EF Core memory leak.   
    // See https://github.com/dotnet/efcore/issues/17623
    public static string GetStatusValue(LeaveEntryStatus status)
    {
        return Enum.GetName(typeof(LeaveEntryStatus), status);
    }
}
