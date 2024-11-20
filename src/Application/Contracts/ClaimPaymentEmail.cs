namespace Engage.Application.Contracts;
public record ClaimPaymentEmail() : BaseContract
{
    public string ToEmailAddress { get; set; }
    public string FromEmailName { get; set; }
    public string FromEmailAddress { get; set; }
    public List<string> CcEmailAddresses { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
