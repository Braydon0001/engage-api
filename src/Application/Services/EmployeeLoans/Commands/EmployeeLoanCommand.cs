namespace Engage.Application.Services.EmployeeLoans.Commands;

public class EmployeeLoanCommand : IMapTo<EmployeeLoan>
{
    public float Amount { get; set; }
    public float RepayableAmount { get; set; }
    public int LoanTerm { get; set; }
    public DateTime LoanDate { get; set; }
    public float Installment { get; set; }
    public string Reason { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<EmployeeLoanCommand, EmployeeLoan>();
    }
}
