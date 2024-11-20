namespace Engage.Application.Services.EmploymentTypes.Commands;

public class EmploymentTypeValidator<T> : AbstractValidator<T> where T : EmploymentTypeCommand
{
    public EmploymentTypeValidator()
    {
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.EndDateReminderDays).GreaterThanOrEqualTo(0);
    }
}

public class CreateEmploymentTypeValidator : EmploymentTypeValidator<CreateEmploymentTypeCommand>
{
}

public class UpdateEmploymentTypeValidator : EmploymentTypeValidator<UpdateEmploymentTypeCommand>
{
    public UpdateEmploymentTypeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
