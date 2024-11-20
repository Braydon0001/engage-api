namespace Engage.Application.Services.EmployeeBadges.Commands
{
    public class EmployeeBadgeCreateCommand : EmployeeBadgeCommand, IRequest<OperationStatus>
    {
    }

    public class EmployeeBadgeCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeBadgeCreateCommand, OperationStatus>
    {
        public EmployeeBadgeCreateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
        {
        }

        public async Task<OperationStatus> Handle(EmployeeBadgeCreateCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EmployeeBadgeCreateCommand, EmployeeBadge>(command);
            _context.EmployeeBadges.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);

            return opStatus;
        }
    }
}
