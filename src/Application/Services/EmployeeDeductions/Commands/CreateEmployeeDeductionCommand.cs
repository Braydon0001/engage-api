namespace Engage.Application.Services.EmployeeDeductions.Commands;

public class CreateEmployeeDeductionCommand : EmployeeDeductionCommand, IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
}

public class CreateEmployeeDeductionCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeDeductionCommand, OperationStatus>
{
    public CreateEmployeeDeductionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateEmployeeDeductionCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEmployeeDeductionCommand, EmployeeDeduction>(command);
        entity.EmployeeId = command.EmployeeId;
        _context.EmployeeDeductions.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeDeductionId;
        return opStatus;
    }
}
