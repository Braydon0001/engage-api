namespace Engage.Domain.Entities;

public class EmployeeStoreCheckIn : BaseAuditableEntity
{
    public int EmployeeStoreCheckInId { get; set; }
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
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
    public string CheckInUuid { get; set; }

    // Navigation Property
    public Employee Employee { get; set; }
    public Store Store { get; set; }
}
