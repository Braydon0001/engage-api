namespace Engage.Application.Services.EmployeeFuels.Commands;

public class EmployeeFuelFileUploadCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public IFormFile File { get; set; }
}

public class EmployeeFuelFileUploadHandler : IRequestHandler<EmployeeFuelFileUploadCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IBlobService _blob;

    public EmployeeFuelFileUploadHandler(IAppDbContext context, IMediator mediator, IBlobService blob)
    {
        _context = context;
        _mediator = mediator;
        _blob = blob;
    }

    public async Task<OperationStatus> Handle(EmployeeFuelFileUploadCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeFuels.SingleOrDefaultAsync(e => e.EmployeeFuelId == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(EmployeeFuel), request.Id);
        }

        var file = request.File;
        var fileName = _blob.CreateFileName(request.Id, file.FileName);
        var uri = await _blob.UploadAsync(file.OpenReadStream(), "employeefuel", fileName, cancellationToken);
        entity.BlobUrl = uri.AbsoluteUri;
        entity.BlobName = fileName;

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeFuelId;
        return opStatus; ;
    }
}