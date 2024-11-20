namespace Engage.Application.Services.FileUploads.Commands;

public class UpdateFileUploadImportDateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateFileUploadImportDateCommandHandler : IRequestHandler<UpdateFileUploadImportDateCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IDateTimeService _dateTime;

    public UpdateFileUploadImportDateCommandHandler(IAppDbContext context, IDateTimeService dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<OperationStatus> Handle(UpdateFileUploadImportDateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileUploads.SingleAsync(e => e.FileUploadId == request.Id, cancellationToken);
        entity.ImportDate = _dateTime.Now;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = request.Id;
        return operationStatus;
    }
}
