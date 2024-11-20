namespace Engage.Application.Services.IncidentSkus.Commands;

public class IncidentSkuValidator<T> : AbstractValidator<T> where T : IncidentSkuCommand
{
    public IncidentSkuValidator()
    {
        RuleFor(x => x.IncidentId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.IncidentSkuTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.IncidentSkuStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.DCProductId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Note).MaximumLength(300).NotEmpty();
    }
}

public class CreateIncidentSkuValidator : IncidentSkuValidator<CreateIncidentSkuCommand>
{
}

public class UpdateIncidentSkuValidator : IncidentSkuValidator<UpdateIncidentSkuCommand>
{
    public UpdateIncidentSkuValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
