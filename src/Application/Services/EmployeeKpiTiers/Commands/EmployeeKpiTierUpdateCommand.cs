namespace Engage.Application.Services.EmployeeKpiTiers.Commands;

public class EmployeeKpiTierUpdateCommand : EmployeeKpiTierCommand, IRequest<OperationStatus>
{
}

public class EmployeeKpiTierUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeKpiTierUpdateCommand, OperationStatus>
{
    public EmployeeKpiTierUpdateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeKpiTierUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeKpiTiers.SingleAsync(x => x.EmployeeKpiTierId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}
