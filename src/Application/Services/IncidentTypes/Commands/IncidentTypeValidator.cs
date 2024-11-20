namespace Engage.Application.Services.IncidentTypes.Commands;

public class IncidentTypeValidator<T> : AbstractValidator<T> where T : IncidentTypeCommand
{
    public IncidentTypeValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
    }
}

public class CreateIncidentTypeValidator : IncidentTypeValidator<CreateIncidentTypeCommand>
{
}

public class UpdateIncidentTypeValidator : IncidentTypeValidator<UpdateIncidentTypeCommand>
{
    public UpdateIncidentTypeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
