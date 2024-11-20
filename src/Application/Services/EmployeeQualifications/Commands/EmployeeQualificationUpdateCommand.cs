namespace Engage.Application.Services.EmployeeQualifications.Commands;

public class EmployeeQualificationUpdateCommand : EmployeeQualificationCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class EmployeeQualificationUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeQualificationUpdateCommand, OperationStatus>
{
    public EmployeeQualificationUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(EmployeeQualificationUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeQualifications.SingleAsync(x => x.EmployeeQualificationId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeQualificationId;
        return opStatus;
    }
}
