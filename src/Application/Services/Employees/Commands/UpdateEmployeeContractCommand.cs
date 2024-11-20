namespace Engage.Application.Services.Employees.Commands;

public class UpdateEmployeeContractCommand : EmployeeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeContractCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeContractCommand, OperationStatus>
{
    public UpdateEmployeeContractCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEmployeeContractCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.SingleAsync(e => e.EmployeeId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeId;
        return opStatus;
    }
}

public class UpdateEmployeeContractValidator : AbstractValidator<UpdateEmployeeContractCommand>
{
    public UpdateEmployeeContractValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}
