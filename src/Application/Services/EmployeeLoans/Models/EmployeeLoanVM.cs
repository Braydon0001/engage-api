namespace Engage.Application.Services.EmployeeLoans.Models;

public class EmployeeLoanVm : IMapFrom<EmployeeLoan>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public float Amount { get; set; }
    public float RepayableAmount { get; set; }
    public int LoanTerm { get; set; }
    public DateTime LoanDate { get; set; }
    public float Installment { get; set; }
    public string Reason { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<EmployeeLoan, EmployeeLoanVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeLoanId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, s.Employee.FirstName + " " + s.Employee.LastName)));
    }
}
