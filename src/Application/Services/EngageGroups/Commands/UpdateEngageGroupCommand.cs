namespace Engage.Application.Services.EngageGroups.Commands;

public class UpdateEngageGroupCommand : EngageGroupCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateUpdateEngageGroupCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEngageGroupCommand, OperationStatus>
{
    public UpdateUpdateEngageGroupCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEngageGroupCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageGroups.SingleAsync(x => x.Id == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}
