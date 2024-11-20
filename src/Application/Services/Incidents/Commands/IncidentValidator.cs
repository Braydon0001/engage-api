namespace Engage.Application.Services.Incidents.Commands;

public class IncidentValidator<T> : AbstractValidator<T> where T : IncidentCommand
{
    public IncidentValidator()
    {
        RuleFor(x => x.ClientTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.IncidentTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.IncidentStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.SupplierId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.IncidentNumber).MaximumLength(20).NotEmpty();
        RuleFor(x => x.IncidentDate).NotEmpty();
        RuleFor(x => x.Note).MaximumLength(300);
    }
}

public class CreateIncidentValidator : IncidentValidator<CreateIncidentCommand>
{
}

public class UpdateIncidentValidator : IncidentValidator<UpdateIncidentCommand>
{
    public UpdateIncidentValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
