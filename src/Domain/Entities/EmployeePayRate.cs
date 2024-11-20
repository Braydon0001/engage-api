namespace Engage.Domain.Entities;
public class EmployeePayRate : BaseAuditableEntity
{
    public int EmployeePayRateId { get; set; }
    public DateTime EffectiveDate { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeePayRateFrequencyId { get; set; }
    public int EmployeePayRatePackageId { get; set; }    
    public string IncreaseReason { get; set; }
    public decimal Amount { get; set; }
    public bool IsPayPackageAutomatically { get; set; }
    public decimal HoursPerDay { get; set; }
    public int DaysPerPeriod { get; set; }
    public decimal HoursPerMonth { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal DailyRate { get; set; }
    public decimal MonthlyRate { get; set; }
    public bool IsWorkMonday { get; set; }
    public bool IsWorkTuesday { get; set; }
    public bool IsWorkWednesday { get; set; }
    public bool IsWorkThursday { get; set; }
    public bool IsWorkFriday { get; set; }
    public bool IsWorkSaturday { get; set; }
    public bool IsWorkSunday { get; set; }
    public string Note { get; set; }

    //Navigation Properties
    public Employee Employee { get; set; }
    public EmployeePayRateFrequency EmployeePayRateFrequency { get; set; }
    public EmployeePayRatePackage EmployeePayRatePackage { get; set; }
}
