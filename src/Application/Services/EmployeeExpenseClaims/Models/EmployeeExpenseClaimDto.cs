namespace Engage.Application.Services.EmployeeExpenseClaims.Models;

public class EmployeeExpenseClaimDto : IMapFrom<EmployeeExpenseClaim>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string Description { get; set; }
    public string RecoverFrom { get; set; }
    public int Value { get; set; }
    public int KMDistanse { get; set; }
    public int? StatusId { get; set; }
    public string StatusName { get; set; }
    public string ManagerComment { get; set; }
    public bool Processed { get; set; }
    public DateTime ClaimDate { get; set; }
    public DateTime SubmittedDate { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeExpenseClaim, EmployeeExpenseClaimDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.EmployeeExpenseClaimId))
            .ForMember(e => e.StatusId, opt => opt.MapFrom(d => d.Status));
    }

    // Use static mathod. See https://github.com/dotnet/efcore/issues/17623
    //public static string GetStatusValue(EmployeeExpenseClaimStatus status)
    //{
    //    return Enum.GetName(typeof(EmployeeExpenseClaimStatus), status);
    //}
}
