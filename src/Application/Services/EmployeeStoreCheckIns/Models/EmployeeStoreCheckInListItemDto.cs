namespace Engage.Application.Services.EmployeeStoreCheckIns.Models;

public class EmployeeStoreCheckInListItemDto : IMapFrom<EmployeeStoreCheckIn>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public DateTime CheckInUTCDate { get; set; }
    public DateTime? CheckOutUTCDate { get; set; }
    public int UTCMinutesInStore { get; set; }
    public int CheckInMetresFromStore { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCheckIn, EmployeeStoreCheckInListItemDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCheckInId))
            .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
            .ForMember(d => d.UTCMinutesInStore, opt => opt.MapFrom(s => DateUtils.MinutesBetweenDates(s.CheckInUTCDate, s.CheckOutUTCDate)))
            .ForMember(d => d.CheckInMetresFromStore, opt => opt.MapFrom(s => Convert.ToInt32(s.CheckInDistance)));

    }
}
