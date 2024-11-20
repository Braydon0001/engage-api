namespace Engage.Application.Services.DCDepartments.Commands;

public class DCDepartmentValidator<T> : AbstractValidator<T> where T : DCDepartmentCommand
{
    public DCDepartmentValidator()
    {
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
    }
}

public class CreateDCDepartmentValidator : DCDepartmentValidator<CreateDCDepartmentCommand>
{
}

public class UpdateDCDepartmentValidator : DCDepartmentValidator<UpdateDCDepartmentCommand>
{
    public UpdateDCDepartmentValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
