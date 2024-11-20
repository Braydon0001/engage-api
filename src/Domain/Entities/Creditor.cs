namespace Engage.Domain.Entities;

public class Creditor : BaseAuditableEntity
{
    public Creditor()
    {
        CreditorStatusHistories = new HashSet<CreditorStatusHistory>();
    }
    public int CreditorId { get; set; }
    public int CreditorStatusId { get; set; }
    public string Name { get; set; }
    public string TradingName { get; set; }
    public bool IsVatRegistered { get; set; }
    public string VatNumber { get; set; }
    public DateTime BankConfirmationDate { get; set; }
    public string EvolutionCreditorNumber { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public CreditorStatus CreditorStatus { get; set; }
    public ICollection<CreditorStatusHistory> CreditorStatusHistories { get; set; }
}