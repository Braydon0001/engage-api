namespace Engage.Application.Services.EmployeeDeductions.Commands;

public class EmployeeDeductionValidator<T> : AbstractValidator<T> where T : EmployeeDeductionCommand
{
    public EmployeeDeductionValidator()
    {
        RuleFor(x => x.DeductionTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.DeductionCycleTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Amount).GreaterThan(0).NotEmpty();
        RuleFor(x => x.DeductionDate).NotEmpty();
        RuleFor(x => x.Reference).MaximumLength(120).NotEmpty();
    }
}

public class CreateEmployeeDeductionValidator : EmployeeDeductionValidator<CreateEmployeeDeductionCommand>
{
    public CreateEmployeeDeductionValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeDeductionValidator : EmployeeDeductionValidator<UpdateEmployeeDeductionCommand>
{
    public UpdateEmployeeDeductionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
