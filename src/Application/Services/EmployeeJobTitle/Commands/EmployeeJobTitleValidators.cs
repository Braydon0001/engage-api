namespace Engage.Application.Services.EmployeeJobTitles.Commands;

public class EmployeeJobTitleValidator<T> : AbstractValidator<T> where T : EmployeeJobTitleCommand
{
    public EmployeeJobTitleValidator()
    {
        RuleFor(e => e.ParentId).GreaterThan(0);
        RuleFor(e => e.Level).GreaterThan(0).NotEmpty();
        RuleFor(e => e.Name).MaximumLength(120).NotEmpty();
        RuleFor(e => e.Description).MaximumLength(120);
        RuleFor(e => e.Order).GreaterThan(0);
    }
}

public class EmployeeJobTitleCreateValidator : EmployeeJobTitleValidator<EmployeeJobTitleCreateCommand>
{
    public EmployeeJobTitleCreateValidator()
    {
    }
}

public class EmployeeJobTitleUpdateValidator : EmployeeJobTitleValidator<EmployeeJobTitleUpdateCommand>
{
    public EmployeeJobTitleUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
