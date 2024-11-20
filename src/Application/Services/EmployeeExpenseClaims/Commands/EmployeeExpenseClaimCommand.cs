namespace Engage.Application.Services.EmployeeExpenseClaims.Commands;

public class EmployeeExpenseClaimCommand : IMapTo<EmployeeExpenseClaim>
{
    public int? StatusId { get; set; }
    public string Description { get; set; }
    public string RecoverFrom { get; set; }
    public int KMDistanse { get; set; }
    public int Value { get; set; }
    public DateTime ClaimDate { get; set; }
    public DateTime SubmittedDate { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string ManagerComment { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeExpenseClaimCommand, EmployeeExpenseClaim>();
    }
}
