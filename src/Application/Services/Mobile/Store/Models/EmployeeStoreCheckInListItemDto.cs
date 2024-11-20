namespace Engage.Application.Services.Mobile.Stores.Models;

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
            .ForMember(d => d.UTCMinutesInStore, opt => opt.MapFrom(s => DateUtils.MinutesBetweenDates(s.CheckInTimezoneDate, s.CheckOutTimezoneDate.HasValue ? s.CheckOutTimezoneDate.Value : s.CheckOutUTCDate.Value)))
            .ForMember(d => d.CheckInMetresFromStore, opt => opt.MapFrom(s => Convert.ToInt32(s.CheckInDistance)))
            .ForMember(d => d.CheckInUTCDate, opt => opt.MapFrom(s => s.CheckInTimezoneDate))
            .ForMember(d => d.CheckOutUTCDate, opt => opt.MapFrom(s => s.CheckOutTimezoneDate));
    }
}
