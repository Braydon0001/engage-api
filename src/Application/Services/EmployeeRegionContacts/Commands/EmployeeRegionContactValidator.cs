namespace Engage.Application.Services.EmployeeRegionContacts.Commands;

public class EmployeeRegionContactValidator<T> : AbstractValidator<T> where T : EmployeeRegionContactCommand
{
    public EmployeeRegionContactValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.MobilePhone).MaximumLength(30);
    }
}

public class CreateEmployeeRegionContactValidator : EmployeeRegionContactValidator<EmployeeRegionContactCreateCommand>
{
    public CreateEmployeeRegionContactValidator()
    {
        RuleFor(x => x.EngageRegionId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeRegionContactValidator : EmployeeRegionContactValidator<EmployeeRegionContactUpdateCommand>
{
    public UpdateEmployeeRegionContactValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
