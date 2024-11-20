namespace Engage.Application.Services.EmployeeStoreCheckIns.Commands;

public class EmployeeStoreCheckInCommand2 : IMapTo<EmployeeStoreCheckIn>
{
    public DateTime CheckInTimezoneDate { get; set; }
    public DateTime CheckInUTCDate { get; set; }
    public float CheckInLat { get; set; }
    public float CheckInLong { get; set; }
    public float CheckInDistance { get; set; }
    public DateTime? CheckOutTimezoneDate { get; set; }
    public DateTime? CheckOutUTCDate { get; set; }
    public float? CheckOutLat { get; set; }
    public float? CheckOutLong { get; set; }
    public float? CheckOutDistance { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<EmployeeStoreCheckInCommand2, EmployeeStoreCheckIn>();
    }
}
