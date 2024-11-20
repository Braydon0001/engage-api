namespace Engage.Application.Services.DCAccounts.Commands;

public class DCAccountValidator<T> : AbstractValidator<T> where T : DCAccountCommand
{
    public DCAccountValidator()
    {
        RuleFor(x => x.AccountNumber).MaximumLength(120).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(220);
    }
}

public class CreateDCAccountValidator : DCAccountValidator<CreateDCAccountCommand>
{
}

public class UpdateDCAccountValidator : DCAccountValidator<UpdateDCAccountCommand>
{
    public UpdateDCAccountValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
