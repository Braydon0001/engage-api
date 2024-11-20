namespace Engage.Application.Services.EmployeeVehicles.Commands;

public class EmployeeVehicleValidator<T> : AbstractValidator<T> where T : EmployeeVehicleCommand
{
    public EmployeeVehicleValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.VehicleTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.VehicleBrandId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.AssetStatusId).GreaterThan(0).NotEmpty(); ;
        RuleFor(x => x.AssetOwnerId).GreaterThan(0).NotEmpty(); ;
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.RegistrationNumber).MaximumLength(100).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(100);
        RuleFor(x => x.Tracker).MaximumLength(100);
        RuleFor(x => x.Year).MaximumLength(100);
        RuleFor(x => x.Vin).MaximumLength(100);
        RuleFor(x => x.Note).MaximumLength(200);
    }
}