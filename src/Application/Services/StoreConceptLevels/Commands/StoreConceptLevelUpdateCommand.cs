namespace Engage.Application.Services.StoreConceptLevels.Commands;

public class StoreConceptLevelUpdateCommand : StoreConceptLevelCommand, IRequest<OperationStatus>
{
}
public class StoreConceptLevelUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<StoreConceptLevelUpdateCommand, OperationStatus>
{
    public StoreConceptLevelUpdateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptLevelUpdateCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.StoreConceptLevels.SingleAsync(x => x.StoreConceptLevelId == request.Id, cancellationToken);
        if (request.StoreConceptId == 0)
        {
            var concept = await _context.StoreConcepts.SingleAsync(x => x.Name == request.StoreConceptName, cancellationToken);
            request.StoreConceptId = concept.Id;
        }
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreConceptLevelId;
        return opStatus;
    }
}

