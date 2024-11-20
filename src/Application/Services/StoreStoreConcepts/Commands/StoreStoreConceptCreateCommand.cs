namespace Engage.Application.Services.StoreStoreConcepts.Commands;

public class StoreStoreConceptCreateCommand : StoreStoreConceptCommand, IRequest<OperationStatus>
{
}
public class StoreStoreConceptCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<StoreStoreConceptCreateCommand, OperationStatus>
{
    public StoreStoreConceptCreateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreStoreConceptCreateCommand request, CancellationToken cancellationToken)
    {

        var concept = await _context.StoreConcepts.SingleAsync(x => x.Name == request.StoreConceptName, cancellationToken);
        //var entity = await _context.StoreStoreConcepts.SingleAsync(x => x.StoreConceptId == concept.Id && x.StoreId == request.StoreId, cancellationToken);
        var entity = _mapper.Map<StoreStoreConceptCreateCommand, StoreStoreConcept>(request);
        entity.StoreConceptId = concept.Id;
        _context.StoreStoreConcepts.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreStoreConceptId;
        return opStatus;
    }
}

