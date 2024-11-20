namespace Engage.Application.Services.WebFiles.Commands;

public class WebFileUpdateCommand : WebFileCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class WebFileUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<WebFileUpdateCommand, OperationStatus>
{
    public WebFileUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(WebFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        if (!string.IsNullOrWhiteSpace(command.FileUrl))
        {
            entity.Files = new List<JsonFile> {
                new JsonFile(command.FileUrl)
            };
        }

        _mapper.Map(command, entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class WebFileUpdateValidator : WebFileValidator<WebFileUpdateCommand>
{
    public WebFileUpdateValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0);
    }
}
