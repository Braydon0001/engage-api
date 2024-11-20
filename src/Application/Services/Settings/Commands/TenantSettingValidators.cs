namespace Engage.Application.Services.Settings.Commands;

public class UpsertTenantSettingValidator : AbstractValidator<UpsertTenantSettingCommand>
{
    public UpsertTenantSettingValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
        RuleFor(x => x.Value).MaximumLength(200).NotEmpty();
    }
}
