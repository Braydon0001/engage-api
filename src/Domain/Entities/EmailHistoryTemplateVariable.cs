namespace Engage.Domain.Entities;

public class EmailHistoryTemplateVariable : BaseAuditableEntity
{
    public EmailHistoryTemplateVariable()
    {
        ClaimNumbers = new HashSet<EmailTemplateVariableClaimNumber>();
    }
    public int EmailHistoryTemplateVariableId { get; set; }
    public int EmailHistoryId { get; set; }
    public string Name { get; set; }
    public string ApproverName { get; set; }
    public string ClaimNumber { get; set; }
    public string RejectedReason { get; set; }
    public string DisputedReason { get; set; }
    public string CutOffDate { get; set; }
    public decimal? TotalAmount { get; set; }
    public string StoreName { get; set; }
    public string EmployeeName { get; set; }
    public string TerminationDate { get; set; }
    public string TerminationReason { get; set; }
    public string TerminatorName { get; set; }
    public int? EmployeeId { get; set; }
    public DateTime? ReportDate { get; set; }
    public int? SurveyInstanceId { get; set; }
    public int? OrderId { get; set; }
    public DateTime? OrderDate { get; set; }
    //public int? OrderStagingId { get; set; }
    public ICollection<EmailTemplateVariableClaimNumber> ClaimNumbers { get; set; }

    public EmailHistory EmailHistory { get; set; }
}
