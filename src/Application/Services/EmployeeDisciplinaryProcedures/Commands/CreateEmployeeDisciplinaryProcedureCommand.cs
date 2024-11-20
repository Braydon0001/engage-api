namespace Engage.Application.Services.EmployeeDisciplinaryProcedures.Commands;

public class CreateEmployeeDisciplinaryProcedureCommand : EmployeeDisciplinaryProcedureCommand, IRequest<OperationStatus>
{
}

public class CreateEmployeeDisciplinaryProcedureCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeDisciplinaryProcedureCommand, OperationStatus>
{
    public CreateEmployeeDisciplinaryProcedureCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateEmployeeDisciplinaryProcedureCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEmployeeDisciplinaryProcedureCommand, EmployeeDisciplinaryProcedure>(command);
        entity.EmployeeId = command.EmployeeId;
        _context.EmployeeDisciplinaryProcedures.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeDisciplinaryProcedureId;
        return opStatus;
    }
}
