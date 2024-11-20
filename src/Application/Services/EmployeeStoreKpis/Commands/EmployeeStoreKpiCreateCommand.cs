namespace Engage.Application.Services.EmployeeStoreKpis.Commands
{
    public class EmployeeStoreKpiCreateCommand : EmployeeStoreKpiCommand, IRequest<OperationStatus>
    {
    }

    public class EmployeeStoreKpiCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeStoreKpiCreateCommand, OperationStatus>
    {
        public EmployeeStoreKpiCreateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
        {
        }

        public async Task<OperationStatus> Handle(EmployeeStoreKpiCreateCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EmployeeStoreKpiCreateCommand, EmployeeStoreKpi>(command);
            _context.EmployeeStoreKpis.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);

            return opStatus;
        }
    }
}
