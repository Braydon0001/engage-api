namespace Engage.Application.Services.EmployeeKpis.Commands;

public class EmployeeKpiUpdateCommand : EmployeeKpiCommand, IRequest<OperationStatus>
{
}

public class EmployeeKpiUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeKpiUpdateCommand, OperationStatus>
{
    public EmployeeKpiUpdateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeKpiUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeKpis.SingleAsync(x => x.EmployeeKpiId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}
