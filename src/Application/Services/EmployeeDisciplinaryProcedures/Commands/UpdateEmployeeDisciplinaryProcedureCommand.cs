namespace Engage.Application.Services.EmployeeDisciplinaryProcedures.Commands;

public class UpdateEmployeeDisciplinaryProcedureCommand : EmployeeDisciplinaryProcedureCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeDisciplinaryProcedureCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeDisciplinaryProcedureCommand, OperationStatus>
{
    public UpdateEmployeeDisciplinaryProcedureCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateEmployeeDisciplinaryProcedureCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeDisciplinaryProcedures.SingleAsync(x => x.EmployeeDisciplinaryProcedureId == command.Id);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeDisciplinaryProcedureId;
        return opStatus;
    }
}
