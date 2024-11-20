namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuNoteUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string Note { get; set; }
}

public class ClaimSkuNoteUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderSkuNoteUpdateCommand, OperationStatus>
{
    public ClaimSkuNoteUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderSkuNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderSkus.SingleAsync(e => e.OrderSkuId == command.Id, cancellationToken);
        entity.Note = command.Note;

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.OrderSkuId;
        return opStatus;
    }
}
