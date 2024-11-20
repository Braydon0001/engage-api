namespace Engage.Application.Services.StoreConceptAttributeValues.Commands;

public class StoreConceptAttributeValueRemoveCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class StoreConceptAttributeValueRemoveCommandHandler : IRequestHandler<StoreConceptAttributeValueRemoveCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public StoreConceptAttributeValueRemoveCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeValueRemoveCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreConceptAttributeValues.SingleAsync(e => e.StoreConceptAttributeValueId == request.Id, cancellationToken);

        _context.StoreConceptAttributeValues.Remove(entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = request.Id;
        return operationStatus;
    }
}
