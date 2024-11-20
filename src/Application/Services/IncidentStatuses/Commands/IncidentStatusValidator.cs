namespace Engage.Application.Services.IncidentStatuses.Commands;

public class IncidentStatusValidator<T> : AbstractValidator<T> where T : IncidentStatusCommand
{
    public IncidentStatusValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
    }
}

public class CreateIncidentStatusValidator : IncidentStatusValidator<CreateIncidentStatusCommand>
{
}

public class UpdateIncidentStatusValidator : IncidentStatusValidator<UpdateIncidentStatusCommand>
{
    public UpdateIncidentStatusValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
