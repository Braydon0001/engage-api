namespace Engage.Application.Services.ClaimBlobs.Commands;

public class UploadClaimBlobCommand : IRequest<OperationStatus>
{
    public int ClaimId { get; set; }
    public IFormFile File { get; set; }
}

public class UploadClaimBlobCommandHandler : IRequestHandler<UploadClaimBlobCommand, OperationStatus>
{
    private readonly IMediator _mediator;
    private readonly IBlobService _blob;

    public UploadClaimBlobCommandHandler(IMediator mediator, IBlobService blob)
    {
        _mediator = mediator;
        _blob = blob;
    }

    public async Task<OperationStatus> Handle(UploadClaimBlobCommand request, CancellationToken cancellationToken)
    {

        var folderName = "claims";
        var fileName = _blob.CreateFileName(request.ClaimId, request.File.FileName);

        var uri = await _blob.UploadAsync(request.File.OpenReadStream(), folderName, fileName, cancellationToken);

        return await _mediator.Send(new CreateClaimBlobCommand
        {
            ClaimId = request.ClaimId,
            FolderName = folderName,
            OriginalFileName = request.File.FileName,
            FileName = fileName,
            Url = uri.AbsoluteUri
        }, cancellationToken);
    }
}
