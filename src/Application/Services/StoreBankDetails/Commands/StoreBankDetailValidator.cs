namespace Engage.Application.Services.StoreBankDetails.Commands;

public class StoreBankDetailValidator<T> : AbstractValidator<T> where T : StoreBankDetailCommand
{
    public StoreBankDetailValidator()
    {

        RuleFor(x => x.Bank).MaximumLength(120).NotEmpty();
        RuleFor(x => x.BranchCode).MaximumLength(30).NotEmpty();
        RuleFor(x => x.AccountNumber).MaximumLength(30).NotEmpty();
        RuleFor(x => x.AccountHolder).MaximumLength(120).NotEmpty();
    }
}

public class CreateStoreBankDetailCommandValidator : StoreBankDetailValidator<CreateStoreBankDetailCommand>
{
    public CreateStoreBankDetailCommandValidator()
    {
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateStoreBankDetailValidator : StoreBankDetailValidator<UpdateStoreBankDetailCommand>
{
    public UpdateStoreBankDetailValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
