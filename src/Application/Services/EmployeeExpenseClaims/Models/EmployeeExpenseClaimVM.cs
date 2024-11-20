namespace Engage.Application.Services.EmployeeExpenseClaims.Models;

public class EmployeeExpenseClaimVm : IMapFrom<EmployeeExpenseClaim>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto StatusId { get; set; }
    public string Description { get; set; }
    public string RecoverFrom { get; set; }
    public int Value { get; set; }
    public int KMDistanse { get; set; }
    public string ManagerComment { get; set; }
    public bool Processed { get; set; }
    public DateTime ClaimDate { get; set; }
    public DateTime SubmittedDate { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<EmployeeExpenseClaim, EmployeeExpenseClaimVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeExpenseClaimId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, s.Employee.FirstName + " " + s.Employee.LastName)))
            .ForMember(d => d.StatusId, opt => opt.MapFrom(s => s.StatusId.HasValue
                                                                          ? new OptionDto(s.StatusId.Value, s.Status.Name)
                                                                          : null));
    }
}
