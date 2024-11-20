namespace Engage.Domain.Entities;

public class CreditorCutOffSetting : BaseAuditableEntity
{
    public int CreditorCutOffSettingId { get; set; }
    public string CreditorCutOff { get; set; }
    public string PaymentCutOff { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}