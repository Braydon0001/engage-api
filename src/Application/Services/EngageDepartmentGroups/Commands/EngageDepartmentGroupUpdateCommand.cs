namespace Engage.Application.Services.EngageDepartmentGroups.Commands;

public class EngageDepartmentGroupUpdateCommand : EngageDepartmentGroupCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class EngageDepartmentGroupUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<EngageDepartmentGroupUpdateCommand, OperationStatus>
{
    public EngageDepartmentGroupUpdateHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(EngageDepartmentGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageDepartmentGroups.SingleAsync(x => x.Id == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}
