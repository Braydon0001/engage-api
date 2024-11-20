namespace Engage.Application.Services.FileTypes.Commands;

public class FileTypeUpdateCommand : FileTypeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class FileTypeUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<FileTypeUpdateCommand, OperationStatus>
{
    public FileTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(FileTypeUpdateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileTypes.SingleOrDefaultAsync(x => x.FileTypeId == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(FileType), cancellationToken);
        }

        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.FileTypeId;
        return opStatus;
    }
}
