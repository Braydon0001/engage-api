namespace Engage.Application.Services.IncidentSkuStatuses.Commands;

public class IncidentSkuStatusValidator<T> : AbstractValidator<T> where T : IncidentSkuStatusCommand
{
    public IncidentSkuStatusValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
    }
}

public class CreateIncidentSkuStatusValidator : IncidentSkuStatusValidator<CreateIncidentSkuStatusCommand>
{
}

public class UpdateIncidentSkuStatusValidator : IncidentSkuStatusValidator<UpdateIncidentSkuStatusCommand>
{
    public UpdateIncidentSkuStatusValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
