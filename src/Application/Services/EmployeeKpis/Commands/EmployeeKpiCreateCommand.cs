namespace Engage.Application.Services.EmployeeKpis.Commands
{
    public class EmployeeKpiCreateCommand : EmployeeKpiCommand, IRequest<OperationStatus>
    {
    }

    public class EmployeeKpiCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeKpiCreateCommand, OperationStatus>
    {
        public EmployeeKpiCreateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
        {
        }

        public async Task<OperationStatus> Handle(EmployeeKpiCreateCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EmployeeKpiCreateCommand, EmployeeKpi>(command);
            _context.EmployeeKpis.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);

            return opStatus;
        }
    }
}
