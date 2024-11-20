namespace Engage.Application.Services.EmployeeDeductions.Commands;

public class UpdateEmployeeDeductionCommand : EmployeeDeductionCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeDeductionCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeDeductionCommand, OperationStatus>
{
    public UpdateEmployeeDeductionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateEmployeeDeductionCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeDeductions.SingleAsync(x => x.EmployeeDeductionId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeDeductionId;
        return opStatus;
    }
}
