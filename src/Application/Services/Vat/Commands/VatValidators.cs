namespace Engage.Application.Services.Vat.Commands;

public class VatValidator<T> : AbstractValidator<T> where T : VatCommand
{
    public VatValidator()
    {
        RuleFor(x => x.Code).MaximumLength(10).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
    }
}

public class CreateVatValidator : VatValidator<CreateVatCommand>
{
    public CreateVatValidator()
    {
    }
}

public class UpdateVatValidator : VatValidator<UpdateVatCommand>
{
    public UpdateVatValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
