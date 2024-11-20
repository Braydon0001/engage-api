namespace Engage.Application.Services.EmployeeStoreCheckIns.Commands;

public class UpdateEmployeeStoreCheckInCommand2 : EmployeeStoreCheckInCommand2, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeStoreCheckInCommandHandler2 : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeStoreCheckInCommand2, OperationStatus>
{
    public UpdateEmployeeStoreCheckInCommandHandler2(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateEmployeeStoreCheckInCommand2 command, CancellationToken cancellationToken)
    {

        var entity = await _context.EmployeeStoreCheckIns
             .FirstOrDefaultAsync(x => x.EmployeeStoreCheckInId == command.Id);

        return await SaveChangesAsync(command, entity, nameof(EmployeeStoreCheckIns), command.Id, cancellationToken);

    }
}
