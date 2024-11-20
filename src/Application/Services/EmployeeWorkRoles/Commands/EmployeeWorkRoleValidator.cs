namespace Engage.Application.Services.EmployeeWorkRoles.Commands;

public class EmployeeWorkRoleValidator<T> : AbstractValidator<T> where T : EmployeeWorkRoleCommand
{
    public EmployeeWorkRoleValidator()
    {
        RuleFor(x => x.ManagerId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EmploymentTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Title).MaximumLength(300).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate);
        RuleFor(x => x.GradeLevel).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StatusId).GreaterThan(0).NotEmpty();
    }
}

public class CreateEmployeeWorkRoleValidator : EmployeeWorkRoleValidator<CreateEmployeeWorkRoleCommand>
{
    public CreateEmployeeWorkRoleValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeWorkRoleValidator : EmployeeWorkRoleValidator<UpdateEmployeeWorkRoleCommand>
{
    public UpdateEmployeeWorkRoleValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
