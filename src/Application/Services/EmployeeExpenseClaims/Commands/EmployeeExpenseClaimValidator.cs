namespace Engage.Application.Services.EmployeeExpenseClaims.Commands;

public class EmployeeExpenseClaimValidator<T> : AbstractValidator<T> where T : EmployeeExpenseClaimCommand
{
    public EmployeeExpenseClaimValidator()
    {
        RuleFor(x => x.StatusId).IsInEnum();
        RuleFor(x => x.RecoverFrom).MaximumLength(120);
        RuleFor(x => x.KMDistanse).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Value).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ClaimDate).NotEmpty();
        RuleFor(x => x.SubmittedDate).NotEmpty();
    }
}

public class CreateEmployeeExpenseClaimValidator : EmployeeExpenseClaimValidator<CreateEmployeeExpenseClaimCommand>
{
    public CreateEmployeeExpenseClaimValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeExpenseClaimValidator : EmployeeExpenseClaimValidator<UpdateEmployeeExpenseClaimCommand>
{
    public UpdateEmployeeExpenseClaimValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
