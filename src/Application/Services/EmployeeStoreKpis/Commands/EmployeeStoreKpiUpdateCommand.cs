namespace Engage.Application.Services.EmployeeStoreKpis.Commands;

public class EmployeeStoreKpiUpdateCommand : EmployeeStoreKpiCommand, IRequest<OperationStatus>
{
}

public class EmployeeStoreKpiUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeStoreKpiUpdateCommand, OperationStatus>
{
    public EmployeeStoreKpiUpdateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeStoreKpiUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStoreKpis.SingleAsync(x => x.EmployeeId == command.EmployeeId && x.StoreId == command.StoreId && x.EmployeeKpiId == command.EmployeeKpiId, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}
