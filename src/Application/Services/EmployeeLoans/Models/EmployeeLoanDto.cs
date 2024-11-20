namespace Engage.Application.Services.EmployeeLoans.Models;

public class EmployeeLoanDto : IMapFrom<EmployeeLoan>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public float Amount { get; set; }
    public float RepayableAmount { get; set; }
    public int LoanTerm { get; set; }
    public DateTime LoanDate { get; set; }
    public float Installment { get; set; }
    public string Reason { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeLoan, EmployeeLoanDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.EmployeeLoanId));
    }
}
