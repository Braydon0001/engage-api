namespace Engage.Application.Services.EmployeePayRates.Commands
{
    public class EmployeePayRateValidator<T> : AbstractValidator<T> where T : EmployeePayRateCommand
    {
        public EmployeePayRateValidator()
        {
            RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        }
    }

    public class CreateEmployeePayRateValidator : EmployeePayRateValidator<CreateEmployeePayRateCommand>
    {

    }

    public class UpdateEmployeePayRateValidator : EmployeePayRateValidator<UpdateEmployeePayRateCommand>
    {
        public UpdateEmployeePayRateValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
