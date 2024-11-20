namespace Engage.Application.Services.EmployeeAddresses.Commands
{
    public class EmployeeAddressValidator<T> : AbstractValidator<T> where T : EmployeeAddressCommand
    {
        public EmployeeAddressValidator()
        {
            RuleFor(x => x.UnitNumber).MaximumLength(15);
            RuleFor(x => x.ComplexName).MaximumLength(120);
            RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.StreetNumber).MaximumLength(15);
            RuleFor(x => x.StreetName).MaximumLength(120).NotEmpty();
            RuleFor(x => x.Suburb).MaximumLength(120);
            RuleFor(x => x.City).MaximumLength(120);
            RuleFor(x => x.Code).MaximumLength(15);
            RuleFor(x => x.PostalUnitNumber).MaximumLength(15);
            RuleFor(x => x.PostalComplexName).MaximumLength(120);
            RuleFor(x => x.PostalStreetNumber).MaximumLength(15);
            RuleFor(x => x.PostalStreetName).MaximumLength(120);
            RuleFor(x => x.PostalSuburb).MaximumLength(120);
            RuleFor(x => x.PostalCity).MaximumLength(120);
            RuleFor(x => x.PostalCode).MaximumLength(15);
            RuleFor(x => x.CareOfIntermediary).MaximumLength(120);
        }
    }

    public class CreateEmployeeAddressValidator : EmployeeAddressValidator<CreateEmployeeAddressCommand>
    {

    }

    public class UpdateEmployeeAddressValidator : EmployeeAddressValidator<UpdateEmployeeAddressCommand>
    {
        public UpdateEmployeeAddressValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
