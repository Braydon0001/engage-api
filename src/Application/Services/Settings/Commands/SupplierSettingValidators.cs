namespace Engage.Application.Services.Settings.Commands;

public class UpsertSupplierSettingValidator : AbstractValidator<UpsertSupplierSettingCommand>
{
    public UpsertSupplierSettingValidator()
    {
        RuleFor(x => x.SupplierId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
        RuleFor(x => x.Value).MaximumLength(200).NotEmpty();
    }
}
