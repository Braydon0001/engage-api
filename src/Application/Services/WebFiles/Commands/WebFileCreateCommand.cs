namespace Engage.Application.Services.WebFiles.Commands;

public class WebFileCreateCommand : WebFileCommand, IRequest<OperationStatus>
{
}

public class WebFileCreateHandler : BaseCreateCommandHandler, IRequestHandler<WebFileCreateCommand, OperationStatus>
{
    public WebFileCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(WebFileCreateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<WebFileCreateCommand, WebFile>(command);

        if (!string.IsNullOrWhiteSpace(command.FileUrl))
        {
            entity.Files = new List<JsonFile> {
                new JsonFile(command.FileUrl)
            };
        }

        _context.WebFiles.Add(entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.WebFileId;
        return operationStatus;
    }
}

public class CreateWebFileValidator : WebFileValidator<WebFileCreateCommand>
{
    public CreateWebFileValidator()
    {
    }
}