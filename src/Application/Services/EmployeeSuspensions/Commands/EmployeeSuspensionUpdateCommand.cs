namespace Engage.Application.Services.EmployeeSuspensions.Commands;

public class EmployeeSuspensionUpdateCommand : EmployeeSuspensionCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class EmployeeSuspensionUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeSuspensionUpdateCommand, OperationStatus>
{
    public EmployeeSuspensionUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(EmployeeSuspensionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeSuspensions.SingleAsync(x => x.EmployeeSuspensionId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeSuspensionId;
        return opStatus;
    }
}
