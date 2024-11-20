namespace Engage.Application.Services.EmployeeStoreCheckIns.Commands;

public class EmployeeStoreCheckInValidator<T> : AbstractValidator<T> where T : EmployeeStoreCheckInCommand
{
    public EmployeeStoreCheckInValidator()
    {
    }
}

public class CreateEmployeeStoreCheckInValidator : EmployeeStoreCheckInValidator<CreateEmployeeStoreCheckInCommand>
{
    public CreateEmployeeStoreCheckInValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.CheckInTimezoneDate).NotEmpty();
        RuleFor(x => x.CheckInLat).NotEmpty();
        RuleFor(x => x.CheckInLong).NotEmpty();
    }
}

public class UpdateEmployeeStoreCheckInValidator : EmployeeStoreCheckInValidator<UpdateEmployeeStoreCheckInCommand>
{
    public UpdateEmployeeStoreCheckInValidator()
    {
        // RuleFor(x => x.CheckInUuid).NotEmpty();
        RuleFor(x => x.CheckOutTimezoneDate).NotEmpty();
        RuleFor(x => x.CheckOutLat).NotEmpty();
        RuleFor(x => x.CheckOutLong).NotEmpty();
    }
}
