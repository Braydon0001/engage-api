namespace Engage.Application.Services.EmployeeTrainingRecords.Commands;

public class UpdateEmployeeTrainingRecordCommand : EmployeeTrainingRecordCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeTrainingRecordCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeTrainingRecordCommand, OperationStatus>
{
    public UpdateEmployeeTrainingRecordCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateEmployeeTrainingRecordCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeTrainingRecords.SingleAsync(x => x.EmployeeTrainingRecordId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeTrainingRecordId;
        return opStatus;
    }
}
