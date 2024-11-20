namespace Engage.Application.Services.EmployeeLoans.Commands;

public class EmployeeLoanValidator<T> : AbstractValidator<T> where T : EmployeeLoanCommand
{
    public EmployeeLoanValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0).NotEmpty();
        RuleFor(x => x.RepayableAmount).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Installment).GreaterThan(0).NotEmpty();
        RuleFor(x => x.LoanTerm).GreaterThan(0).NotEmpty();
        RuleFor(x => x.LoanDate).NotEmpty();
        RuleFor(x => x.Reason).NotEmpty();
    }
}

public class CreateEmployeeLoanValidator : EmployeeLoanValidator<CreateEmployeeLoanCommand>
{
    public CreateEmployeeLoanValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeLoanValidator : EmployeeLoanValidator<UpdateEmployeeLoanCommand>
{
    public UpdateEmployeeLoanValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
