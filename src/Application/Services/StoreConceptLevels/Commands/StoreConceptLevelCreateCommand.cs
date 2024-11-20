namespace Engage.Application.Services.StoreConceptLevels.Commands;

public class StoreConceptLevelCreateCommand : StoreConceptLevelCommand, IRequest<OperationStatus>
{
}
public class StoreConceptLevelCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<StoreConceptLevelCreateCommand, OperationStatus>
{
    public StoreConceptLevelCreateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptLevelCreateCommand request, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<StoreConceptLevelCreateCommand, StoreConceptLevel>(request);
        if(entity.StoreConceptId == 0)
        {
            var concept = await _context.StoreConcepts.SingleAsync(x => x.Name == request.StoreConceptName, cancellationToken);
            entity.StoreConceptId = concept.Id;
        }
        _context.StoreConceptLevels.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreConceptLevelId;
        return opStatus;
    }
}

