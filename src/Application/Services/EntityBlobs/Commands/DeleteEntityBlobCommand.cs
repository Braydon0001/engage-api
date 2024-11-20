namespace Engage.Application.Services.EntityBlobs.Commands;

public class DeleteEntityBlobCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class DeleteEntityBlobCommandHandler : IRequestHandler<DeleteEntityBlobCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IBlobService _blob;

    public DeleteEntityBlobCommandHandler(IAppDbContext context, IMediator mediator, IBlobService blob)
    {
        _context = context;
        _mediator = mediator;
        _blob = blob;
    }

    public async Task<OperationStatus> Handle(DeleteEntityBlobCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _context.EntityBlobs.SingleAsync(e => e.EntityBlobId == request.Id);

            await _blob.DeleteAsync(entity.FolderName, entity.FileName, cancellationToken);

            return await _mediator.Send(new DeleteCommand
            {
                EntityName = "entityblob",
                Id = request.Id
            });
        }
        catch (BlobException ex)
        {
            return OperationStatus.CreateFromException(ex.Message, ex);
        }
    }
}
