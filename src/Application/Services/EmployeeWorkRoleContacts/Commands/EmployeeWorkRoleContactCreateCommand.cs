namespace Engage.Application.Services.EmployeeWorkRoleContacts.Commands;

public class EmployeeWorkRoleContactCreateCommand : EmployeeWorkRoleContactCommand, IRequest<OperationStatus>
{
    public int EmployeeWorkRoleId { get; set; }
}

public class EmployeeWorkRoleContactCreateHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeWorkRoleContactCreateCommand, OperationStatus>
{
    public EmployeeWorkRoleContactCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(EmployeeWorkRoleContactCreateCommand command, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<EmployeeWorkRoleContactCreateCommand, EmployeeWorkRoleContact>(command);
        entity.EmployeeWorkRoleId = command.EmployeeWorkRoleId;
        _context.EmployeeWorkRoleContacts.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeWorkRoleContactId;
        return opStatus;
    }
}
