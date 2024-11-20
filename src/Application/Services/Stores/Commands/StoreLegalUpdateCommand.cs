namespace Engage.Application.Services.Stores.Commands;

public class StoreLegalUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string VatNumber { get; set; }
}
public class StoreLegalUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<StoreLegalUpdateCommand, OperationStatus>
{
    public StoreLegalUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreLegalUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Stores.SingleAsync(x => x.StoreId == command.Id, cancellationToken);
        if (entity == null)
        {
            throw new Exception("Store Not Found");
        }

        entity.VatNumber = command.VatNumber;

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreId;
        return opStatus;
    }
}
