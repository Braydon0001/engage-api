namespace Engage.Application.Services.EmployeeLeaveEntries.Commands;

public class EmployeeLeaveEntryValidator<T> : AbstractValidator<T> where T : EmployeeLeaveEntryCommand
{
    public EmployeeLeaveEntryValidator()
    {
        RuleFor(x => x.LeaveTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.FromDate).NotEmpty();
        RuleFor(x => x.ToDate).NotEmpty();
        RuleFor(x => x.ProcessedDate).NotEmpty();
    }
}

public class CreateEmployeeLeaveEntryValidator : EmployeeLeaveEntryValidator<CreateEmployeeLeaveEntryCommand>
{
    public CreateEmployeeLeaveEntryValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeLeaveEntryValidator : EmployeeLeaveEntryValidator<UpdateEmployeeLeaveEntryCommand>
{
    public UpdateEmployeeLeaveEntryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
