namespace Engage.Application.Services.EmployeeStoreCheckIns.Commands;

public class CreateEmployeeStoreCheckInCommand2 : EmployeeStoreCheckInCommand2, IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
}

public class CreateEmployeeStoreCheckInCommandHandler2 : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeStoreCheckInCommand2, OperationStatus>
{
    public CreateEmployeeStoreCheckInCommandHandler2(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(CreateEmployeeStoreCheckInCommand2 command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEmployeeStoreCheckInCommand2, EmployeeStoreCheckIn>(command);
        entity.EmployeeId = command.EmployeeId;
        entity.StoreId = command.StoreId;
        _context.EmployeeStoreCheckIns.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeStoreCheckInId;
        return opStatus;

    }
}
