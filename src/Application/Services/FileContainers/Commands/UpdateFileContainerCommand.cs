namespace Engage.Application.Services.FileContainers.Commands;

public class UpdateFileContainerCommand : FileContainerCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateFileContainerCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateFileContainerCommand, OperationStatus>
{
    public UpdateFileContainerCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateFileContainerCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.FileContainers.SingleAsync(x => x.FileContainerId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.FileContainerId;
        return opStatus;
    }
}
