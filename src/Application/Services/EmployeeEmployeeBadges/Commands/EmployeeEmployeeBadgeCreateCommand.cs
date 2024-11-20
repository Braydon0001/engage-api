namespace Engage.Application.Services.EmployeeEmployeeBadges.Commands
{
    public class EmployeeEmployeeBadgeCreateCommand : EmployeeEmployeeBadgeCommand, IRequest<OperationStatus>
    {
    }

    public class EmployeeEmployeeBadgeCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeEmployeeBadgeCreateCommand, OperationStatus>
    {
        public EmployeeEmployeeBadgeCreateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
        {
        }

        public async Task<OperationStatus> Handle(EmployeeEmployeeBadgeCreateCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EmployeeEmployeeBadgeCreateCommand, EmployeeEmployeeBadge>(command);
            _context.EmployeeEmployeeBadges.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);

            return opStatus;
        }
    }
}
