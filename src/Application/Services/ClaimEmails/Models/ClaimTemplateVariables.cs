namespace Engage.Application.Services.ClaimEmails.Models;

public class ClaimTemplateVariables
{
    //TODO Creator
    [JsonProperty("name")]
    public string Name { get; set; }

    //TODO CurrentUser
    [JsonProperty("approverName")]
    public string ApproverName { get; set; }

    //TODO Number
    [JsonProperty("claimNumber")]
    public string ClaimNumber { get; set; }

    //TODOD Reason 
    [JsonProperty("rejectedReason")]
    public string RejectedReason { get; set; }

    [JsonProperty("disputedReason")]
    public string DisputedReason { get; set; }

    [JsonProperty("cutOffDate")]
    public string CutOffDate { get; set; }

    [JsonProperty("totalAmount")]
    public decimal TotalAmount { get; set; }

    [JsonProperty("storeName")]
    public string StoreName { get; set; }

    [JsonProperty("supplierName")]
    public string SupplierName { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("claimNumbers")]
    public List<ClaimNumber> ClaimNumbers { get; set; }

}
