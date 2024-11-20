namespace Engage.Application.Services.EmployeeFuels.Commands;

public class EmployeeFuelFileDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class EmployeeFuelFileDeleteHandler : IRequestHandler<EmployeeFuelFileDeleteCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IBlobService _blob;

    public EmployeeFuelFileDeleteHandler(IAppDbContext context, IMediator mediator, IBlobService blob)
    {
        _context = context;
        _mediator = mediator;
        _blob = blob;
    }

    public async Task<OperationStatus> Handle(EmployeeFuelFileDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeFuels.SingleOrDefaultAsync(e => e.EmployeeFuelId == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(EmployeeFuel), request.Id);
        }

        if (!string.IsNullOrWhiteSpace(entity.BlobName))
        {
            await _blob.DeleteAsync("employeefuel", entity.BlobName, cancellationToken);
            entity.BlobUrl = string.Empty;
            entity.BlobName = string.Empty;

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.EmployeeFuelId;
            return opStatus;
        }

        return new OperationStatus(true);
    }
}