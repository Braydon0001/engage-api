namespace Engage.Application.Services.WebFileCategories.Commands;

public record WebFileCategoryDeleteCommand(int Id) : IRequest<OperationStatus>
{
}

public class WebFileCategoryDeleteHandler : IRequestHandler<WebFileCategoryDeleteCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public WebFileCategoryDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(WebFileCategoryDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFileCategories.SingleOrDefaultAsync(e => e.WebFileCategoryId == request.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        _context.WebFileCategories.Remove(entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = request.Id;
        return operationStatus;
    }
}
