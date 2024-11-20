namespace Engage.Application.Services.VatPeriods.Commands;

public class VatPeriodValidator<T> : AbstractValidator<T> where T : VatPeriodCommand
{
    public VatPeriodValidator()
    {
        RuleFor(x => x.VatId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.Percent).GreaterThanOrEqualTo(0);
    }
}

public class CreateVatPeriodValidator : VatPeriodValidator<CreateVatPeriodCommand>
{
    public CreateVatPeriodValidator()
    {
    }
}

public class UpdateVatPeriodValidator : VatPeriodValidator<UpdateVatPeriodCommand>
{
    public UpdateVatPeriodValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
