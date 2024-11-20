namespace Engage.Application.Services.EngageDepartmentGroups.Commands;

public class EngageDepartmentGroupCreateCommand : EngageDepartmentGroupCommand, IRequest<OperationStatus>
{
}

public class EngageDepartmentGroupCreateHandler : BaseCreateCommandHandler, IRequestHandler<EngageDepartmentGroupCreateCommand, OperationStatus>
{
    public EngageDepartmentGroupCreateHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(EngageDepartmentGroupCreateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EngageDepartmentGroupCreateCommand, EngageDepartmentGroup>(command);
        _context.EngageDepartmentGroups.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}
