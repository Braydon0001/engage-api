namespace Engage.Application.Services.EmployeeLeaveEntries.Commands;

public class CreateEmployeeLeaveEntryCommand : EmployeeLeaveEntryCommand, IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
}

public class CreateEmployeeLeaveEntryCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeLeaveEntryCommand, OperationStatus>
{
    public CreateEmployeeLeaveEntryCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateEmployeeLeaveEntryCommand command, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<CreateEmployeeLeaveEntryCommand, EmployeeLeaveEntry>(command);
        entity.EmployeeId = command.EmployeeId;
        _context.EmployeeLeaveEntries.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeLeaveEntryId;
        return opStatus;
    }
}
