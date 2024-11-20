namespace Engage.Application.Services.EmployeeTrainingRecords.Commands;

public class CreateEmployeeTrainingRecordCommand : EmployeeTrainingRecordCommand, IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
}

public class CreateEmployeeTrainingRecordCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeTrainingRecordCommand, OperationStatus>
{
    public CreateEmployeeTrainingRecordCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateEmployeeTrainingRecordCommand command, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<CreateEmployeeTrainingRecordCommand, EmployeeTrainingRecord>(command);
        entity.EmployeeId = command.EmployeeId;
        _context.EmployeeTrainingRecords.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeTrainingRecordId;
        return opStatus;
    }
}
