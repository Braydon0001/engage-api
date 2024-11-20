namespace Engage.Application.Services.EmployeeStoreCheckIns.Commands;

public class EmployeeStoreCheckInValidator2<T> : AbstractValidator<T> where T : EmployeeStoreCheckInCommand2
{
    public EmployeeStoreCheckInValidator2()
    {
        RuleFor(x => x.CheckInTimezoneDate).NotEmpty();
        RuleFor(x => x.CheckInUTCDate).NotEmpty();
        RuleFor(x => x.CheckInLat).NotEmpty();
        RuleFor(x => x.CheckInLong).NotEmpty();
        RuleFor(x => x.CheckInDistance).NotEmpty();
    }
}

public class CreateEmployeeStoreCheckInValidator2 : EmployeeStoreCheckInValidator2<CreateEmployeeStoreCheckInCommand2>
{
    public CreateEmployeeStoreCheckInValidator2()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeStoreCheckInValidator2 : EmployeeStoreCheckInValidator2<UpdateEmployeeStoreCheckInCommand2>
{
    public UpdateEmployeeStoreCheckInValidator2()
    {

    }
}
