namespace Engage.Application.Services.EmployeeWorkRoleContacts.Commands;

public class EmployeeWorkRoleContactValidator<T> : AbstractValidator<T> where T : EmployeeWorkRoleContactCommand
{
    public EmployeeWorkRoleContactValidator()
    {
        RuleFor(x => x.EmailAddress).MaximumLength(100).NotEmpty();
        RuleFor(x => x.FirstName).MaximumLength(120).NotEmpty();
        RuleFor(x => x.LastName).MaximumLength(120).NotEmpty();
        RuleFor(x => x.MobilePhone).MaximumLength(30);
        RuleFor(x => x.Description).MaximumLength(200);
        RuleFor(x => x.Title).MaximumLength(120);
    }
}

public class CreateEmployeeWorkRoleContactValidator : EmployeeWorkRoleContactValidator<EmployeeWorkRoleContactCreateCommand>
{
    public CreateEmployeeWorkRoleContactValidator()
    {
        RuleFor(x => x.EmployeeWorkRoleId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeWorkRoleContactValidator : EmployeeWorkRoleContactValidator<EmployeeWorkRoleContactUpdateCommand>
{
    public UpdateEmployeeWorkRoleContactValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
