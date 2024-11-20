namespace Engage.Application.Services.EmployeeQualifications.Commands;

public class EmployeeQualificationCreateCommand : EmployeeQualificationCommand, IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
}

public class EmployeeQualificationCreateHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeQualificationCreateCommand, OperationStatus>
{
    public EmployeeQualificationCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(EmployeeQualificationCreateCommand command, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<EmployeeQualificationCreateCommand, EmployeeQualification>(command);
        entity.EmployeeId = command.EmployeeId;
        _context.EmployeeQualifications.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeQualificationId;
        return opStatus;
    }
}
