namespace Engage.Application.Services.EmployeeLeaveEntries.Models
{
    public class EmployeeLeaveEntryVm : IMapFrom<EmployeeLeaveEntry>
    {
        public int Id { get; set; }
        public OptionDto LeaveTypeId { get; set; }
        public LeaveEntryStatus Status { get; set; }
        public DateTime FromDate { get; set; }
        public bool FromHalfDay { get; set; }
        public DateTime ToDate { get; set; }
        public bool ToHalfDay { get; set; }
        public string Comment { get; set; }
        public bool AdjustLeave { get; set; }
        public string ManagerComment { get; set; }
        public bool Processed { get; set; }
        public DateTime ProcessedDate { get; set; }
        public bool Disabled { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmployeeLeaveEntry, EmployeeLeaveEntryVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeLeaveEntryId))
                .ForMember(d => d.LeaveTypeId, opt => opt.MapFrom(s => new OptionDto(s.LeaveTypeId, s.LeaveType.Name)))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => new OptionDto((int)s.Status, Enum.GetName(typeof(LeaveEntryStatus), s.Status))));
        }
    }
}
