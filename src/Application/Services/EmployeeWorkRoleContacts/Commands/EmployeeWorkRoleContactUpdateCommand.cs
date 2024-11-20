namespace Engage.Application.Services.EmployeeWorkRoleContacts.Commands;

public class EmployeeWorkRoleContactUpdateCommand : EmployeeWorkRoleContactCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class EmployeeWorkRoleContactUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeWorkRoleContactUpdateCommand, OperationStatus>
{
    public EmployeeWorkRoleContactUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(EmployeeWorkRoleContactUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeWorkRoleContacts.SingleAsync(x => x.EmployeeWorkRoleContactId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeWorkRoleContactId;
        return opStatus;
    }
}
