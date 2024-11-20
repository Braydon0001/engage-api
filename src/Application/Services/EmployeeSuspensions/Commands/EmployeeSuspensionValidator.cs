namespace Engage.Application.Services.EmployeeSuspensions.Commands;

public class EmployeeSuspensionValidator<T> : AbstractValidator<T> where T : EmployeeSuspensionCommand
{
    public EmployeeSuspensionValidator()
    {
        RuleFor(x => x.EmployeeSuspensionReasonId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.EndDate).NotEmpty();
    }
}

public class CreateEmployeeSuspensionValidator : EmployeeSuspensionValidator<EmployeeSuspensionCreateCommand>
{
    public CreateEmployeeSuspensionValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeSuspensionValidator : EmployeeSuspensionValidator<EmployeeSuspensionUpdateCommand>
{
    public UpdateEmployeeSuspensionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
