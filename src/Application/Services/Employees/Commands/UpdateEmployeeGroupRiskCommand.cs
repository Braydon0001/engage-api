namespace Engage.Application.Services.Employees.Commands;

public class UpdateEmployeeGroupRiskCommand : EmployeeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeGroupRiskCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeGroupRiskCommand, OperationStatus>
{
    public UpdateEmployeeGroupRiskCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEmployeeGroupRiskCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.SingleAsync(e => e.EmployeeId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeId;
        return opStatus;
    }
}

public class UpdateEmployeeGroupRiskValidator : AbstractValidator<UpdateEmployeeGroupRiskCommand>
{
    public UpdateEmployeeGroupRiskValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}
