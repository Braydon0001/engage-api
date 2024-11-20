namespace Engage.Application.Services.FileTypes.Commands;

public class FileTypeCreateCommand : FileTypeCommand, IRequest<OperationStatus>
{
}

public class FileTypeCreateHandler : BaseCreateCommandHandler, IRequestHandler<FileTypeCreateCommand, OperationStatus>
{
    public FileTypeCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(FileTypeCreateCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<FileTypeCreateCommand, FileType>(request);
        _context.FileTypes.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.FileTypeId;
        return opStatus;
    }
}