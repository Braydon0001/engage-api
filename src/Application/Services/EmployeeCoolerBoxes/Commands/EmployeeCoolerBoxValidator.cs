namespace Engage.Application.Services.EmployeeCoolerBoxes.Commands;

public class EmployeeCoolerBoxValidator<T> : AbstractValidator<T> where T : EmployeeCoolerBoxCommand
{
    public EmployeeCoolerBoxValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EmployeeCoolerBoxConditionId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(100);
        RuleFor(x => x.Note).MaximumLength(200);
    }
}