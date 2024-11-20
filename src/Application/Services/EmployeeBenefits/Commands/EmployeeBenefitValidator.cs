namespace Engage.Application.Services.EmployeeBenefits.Commands;

public class EmployeeBenefitValidator<T> : AbstractValidator<T> where T : EmployeeBenefitCommand
{
    public EmployeeBenefitValidator()
    {
        RuleFor(x => x.BenefitTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.Value).GreaterThan(0).NotEmpty();
        RuleFor(x => x.IssuedDate).NotEmpty();
    }
}

public class CreateEmployeeCreateEmployeeBenefitCommandValidator : EmployeeBenefitValidator<CreateEmployeeBenefitCommand>
{
    public CreateEmployeeCreateEmployeeBenefitCommandValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();

    }
}
public class UpdateEmployeeBenefitValidator : EmployeeBenefitValidator<UpdateEmployeeBenefitCommand>
{
    public UpdateEmployeeBenefitValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
