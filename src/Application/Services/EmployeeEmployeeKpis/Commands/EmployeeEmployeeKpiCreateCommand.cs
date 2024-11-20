namespace Engage.Application.Services.EmployeeEmployeeKpis.Commands
{
    public class EmployeeEmployeeKpiCreateCommand : EmployeeEmployeeKpiCommand, IRequest<OperationStatus>
    {
    }

    public class EmployeeEmployeeKpiCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeEmployeeKpiCreateCommand, OperationStatus>
    {
        public EmployeeEmployeeKpiCreateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
        {
        }

        public async Task<OperationStatus> Handle(EmployeeEmployeeKpiCreateCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EmployeeEmployeeKpiCreateCommand, EmployeeEmployeeKpi>(command);
            _context.EmployeeEmployeeKpis.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);

            return opStatus;
        }
    }
}
