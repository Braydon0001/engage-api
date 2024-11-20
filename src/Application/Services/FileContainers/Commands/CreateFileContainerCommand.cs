namespace Engage.Application.Services.FileContainers.Commands;

public class CreateFileContainerCommand : FileContainerCommand, IRequest<OperationStatus>
{
}

public class CreateFileContainerCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateFileContainerCommand, OperationStatus>
{
    public CreateFileContainerCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateFileContainerCommand command, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<CreateFileContainerCommand, FileContainer>(command);
        _context.FileContainers.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.FileContainerId;
        return opStatus;
    }
}
