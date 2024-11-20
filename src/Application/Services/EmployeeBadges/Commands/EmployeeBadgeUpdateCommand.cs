namespace Engage.Application.Services.EmployeeBadges.Commands;

public class EmployeeBadgeUpdateCommand : EmployeeBadgeCommand, IRequest<OperationStatus>
{
}

public class EmployeeBadgeUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeBadgeUpdateCommand, OperationStatus>
{
    public EmployeeBadgeUpdateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeBadgeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeBadges.SingleAsync(x => x.EmployeeBadgeId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}
