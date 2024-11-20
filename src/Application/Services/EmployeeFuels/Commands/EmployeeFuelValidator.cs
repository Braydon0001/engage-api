namespace Engage.Application.Services.EmployeeFuels.Commands;

public class EmployeeFuelValidator<T> : AbstractValidator<T> where T : EmployeeFuelCommand
{
    public EmployeeFuelValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EmployeeVehicleId).GreaterThan(0).NotEmpty();
        //RuleFor(x => x.EmployeePaymentTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EmployeeFuelExpenseTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.FuelDate).NotEmpty();
        //RuleFor(x => x.Amount).GreaterThanOrEqualTo(0).NotEmpty();
    }
}

public class EmployeeFuelCreateValidator : EmployeeFuelValidator<EmployeeFuelCreateCommand>
{
    public EmployeeFuelCreateValidator()
    {

    }
}

public class EmployeeFuelUpdateValidator : EmployeeFuelValidator<EmployeeFuelUpdateCommand>
{
    public EmployeeFuelUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
