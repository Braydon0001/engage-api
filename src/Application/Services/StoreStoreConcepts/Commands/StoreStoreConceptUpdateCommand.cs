namespace Engage.Application.Services.StoreStoreConcepts.Commands;

public class StoreStoreConceptUpdateCommand : StoreStoreConceptCommand, IRequest<OperationStatus>
{
}
public class StoreStoreConceptUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<StoreStoreConceptUpdateCommand, OperationStatus>
{
    public StoreStoreConceptUpdateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreStoreConceptUpdateCommand request, CancellationToken cancellationToken)
    {

        var concept = await _context.StoreConcepts.SingleAsync(x => x.Name == request.StoreConceptName, cancellationToken);
        var entity = await _context.StoreStoreConcepts.SingleAsync(x => x.StoreConceptId == concept.Id && x.StoreId == request.StoreId, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreStoreConceptId;
        return opStatus;
    }
}

