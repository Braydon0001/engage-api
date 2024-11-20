namespace Engage.Application.Services.EmployeeLeaveEntries.Commands;

public class UpdateEmployeeLeaveEntryCommand : EmployeeLeaveEntryCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeLeaveEntryCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeLeaveEntryCommand, OperationStatus>
{
    public UpdateEmployeeLeaveEntryCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateEmployeeLeaveEntryCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeLeaveEntries.SingleAsync(x => x.EmployeeLeaveEntryId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeLeaveEntryId;
        return opStatus;
    }
}
