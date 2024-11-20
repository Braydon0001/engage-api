namespace Engage.Application.Services.EmployeeSuspensions.Commands;

public class EmployeeSuspensionCreateCommand : EmployeeSuspensionCommand, IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
}

public class EmployeeSuspensionCreateHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeSuspensionCreateCommand, OperationStatus>
{
    public EmployeeSuspensionCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(EmployeeSuspensionCreateCommand command, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<EmployeeSuspensionCreateCommand, EmployeeSuspension>(command);
        entity.EmployeeId = command.EmployeeId;
        _context.EmployeeSuspensions.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeSuspensionId;
        return opStatus;
    }
}
