namespace Engage.Application.Services.EmployeeDisciplinaryProcedures.Commands;

public class EmployeeDisciplinaryProcedureValidator<T> : AbstractValidator<T> where T : EmployeeDisciplinaryProcedureCommand
{
    public EmployeeDisciplinaryProcedureValidator()
    {
        RuleFor(x => x.Description).MaximumLength(300).NotEmpty();
    }
}

public class CreateEmployeeDisciplinaryProcedureValidator : EmployeeDisciplinaryProcedureValidator<CreateEmployeeDisciplinaryProcedureCommand>
{
    public CreateEmployeeDisciplinaryProcedureValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeDisciplinaryProcedureValidator : EmployeeDisciplinaryProcedureValidator<UpdateEmployeeDisciplinaryProcedureCommand>
{
    public UpdateEmployeeDisciplinaryProcedureValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();

    }
}
