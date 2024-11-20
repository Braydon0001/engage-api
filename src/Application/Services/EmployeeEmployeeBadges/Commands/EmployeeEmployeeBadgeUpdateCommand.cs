namespace Engage.Application.Services.EmployeeEmployeeBadges.Commands;

public class EmployeeEmployeeBadgeUpdateCommand : EmployeeEmployeeBadgeCommand, IRequest<OperationStatus>
{
}

public class EmployeeEmployeeBadgeUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeEmployeeBadgeUpdateCommand, OperationStatus>
{
    public EmployeeEmployeeBadgeUpdateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeEmployeeBadgeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeEmployeeBadges.SingleAsync(x => x.EmployeeId == command.EmployeeId && x.EmployeeBadgeId == command.EmployeeBadgeId, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}
