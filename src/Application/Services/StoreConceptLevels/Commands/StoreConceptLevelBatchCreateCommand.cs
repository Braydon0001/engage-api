namespace Engage.Application.Services.StoreConceptLevels.Commands;

public class StoreConceptLevelBatchCreateCommand : IRequest<OperationStatus>
{
    public int storeId { get; set; }
    public List<LevelObject> concepts { get; set; }
}
public class StoreConceptLevelBatchCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<StoreConceptLevelBatchCreateCommand, OperationStatus>
{
    public StoreConceptLevelBatchCreateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptLevelBatchCreateCommand request, CancellationToken cancellationToken)
    {
        foreach (var concept in request.concepts)
        {
            var entity = new StoreConceptLevel
            {
                StoreId = request.storeId,
                StoreConceptId = concept.storeConceptId,
                Level = concept.level,
            };
            _context.StoreConceptLevels.Add(entity);
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = 1;
        return opStatus;
    }
}

public class LevelObject
{
    public int storeConceptId { get; set; }
    public int level { get; set; }
}

public class LevelEntity : LevelObject
{
    public int storeId { get; set; }
}

