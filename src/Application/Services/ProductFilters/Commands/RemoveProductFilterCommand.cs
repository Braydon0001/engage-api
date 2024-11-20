namespace Engage.Application.Services.ProductFilters.Commands;

public class RemoveProductFilterCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class RemoveProductFilterCommandHandler : IRequestHandler<RemoveProductFilterCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public RemoveProductFilterCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(RemoveProductFilterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductFilters.SingleAsync(e => e.ProductFilterId == request.Id, cancellationToken);

        _context.ProductFilters.Remove(entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = request.Id;
        return operationStatus;
    }
}
