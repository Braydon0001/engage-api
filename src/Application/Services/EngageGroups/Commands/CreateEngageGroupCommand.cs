namespace Engage.Application.Services.EngageGroups.Commands;

public class CreateEngageGroupCommand : EngageGroupCommand, IRequest<OperationStatus>
{
}

public class CreateAssetCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEngageGroupCommand, OperationStatus>
{
    public CreateAssetCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(CreateEngageGroupCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEngageGroupCommand, EngageGroup>(command);
        _context.EngageGroups.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}
