namespace Engage.Application.Services.IncidentSkuTypes.Commands;

public class IncidentSkuTypeValidator<T> : AbstractValidator<T> where T : IncidentSkuTypeCommand
{
    public IncidentSkuTypeValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
    }
}

public class CreateIncidentSkuTypeValidator : IncidentSkuTypeValidator<CreateIncidentSkuTypeCommand>
{
}

public class UpdateIncidentSkuTypeValidator : IncidentSkuTypeValidator<UpdateIncidentSkuTypeCommand>
{
    public UpdateIncidentSkuTypeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
