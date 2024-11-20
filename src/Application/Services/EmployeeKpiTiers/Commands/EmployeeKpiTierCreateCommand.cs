namespace Engage.Application.Services.EmployeeKpiTiers.Commands
{
    public class EmployeeKpiTierCreateCommand : EmployeeKpiTierCommand, IRequest<OperationStatus>
    {
    }

    public class EmployeeKpiTierCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeKpiTierCreateCommand, OperationStatus>
    {
        public EmployeeKpiTierCreateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
        {
        }

        public async Task<OperationStatus> Handle(EmployeeKpiTierCreateCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EmployeeKpiTierCreateCommand, EmployeeKpiTier>(command);
            _context.EmployeeKpiTiers.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);

            return opStatus;
        }
    }
}
