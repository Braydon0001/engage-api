namespace Engage.Application.Services.EmployeeBankDetails.Commands;

public class EmployeeBankDetailValidator<T> : AbstractValidator<T> where T : EmployeeBankDetailCommand
{
    public EmployeeBankDetailValidator()
    {
        RuleFor(x => x.BankPaymentMethodId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.BankAccountOwnerId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.BankAccountTypeId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.BankNameId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.BranchCode).MaximumLength(30).NotEmpty();
        RuleFor(x => x.AccountNumber).MaximumLength(30).NotEmpty();
        RuleFor(x => x.AccountHolder).MaximumLength(120).NotEmpty();
    }
}

public class CreateEmployeeBankDetailCommandValidator : EmployeeBankDetailValidator<CreateEmployeeBankDetailCommand>
{
    public CreateEmployeeBankDetailCommandValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeBankDetailValidator : EmployeeBankDetailValidator<UpdateEmployeeBankDetailCommand>
{
    public UpdateEmployeeBankDetailValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
