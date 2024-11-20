namespace Engage.Application.Services.ClaimReports.Models;

public class ClaimFNBReport
{
    public string RecipientName { get; set; }
    public string RecipientAccount { get; set; }
    public int RecipientAccountType { get; set; }
    public string BranchCode { get; set; }
    public decimal Amount { get; set; }
    public string OwnReference { get; set; }
    public string RecipientReference { get; set; }
}
