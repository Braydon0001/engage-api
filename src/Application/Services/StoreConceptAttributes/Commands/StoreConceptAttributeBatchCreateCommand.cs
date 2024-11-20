namespace Engage.Application.Services.StoreConceptAttributes.Commands;

public class StoreConceptAttributeBatchCreateCommand : IRequest<OperationStatus>
{
    public int conceptId { get; set; }
    public List<AttributeObject> attributes { get; set; }
}
public class StoreConceptAttributeBatchCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<StoreConceptAttributeBatchCreateCommand, OperationStatus>
{
    public StoreConceptAttributeBatchCreateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeBatchCreateCommand request, CancellationToken cancellationToken)
    {
        foreach (var concept in request.attributes)
        {
            var entity = new StoreConceptAttribute
            {
                StoreConceptId = request.conceptId,
                Name = concept.name,
                StoreConceptAttributeTypeId = concept.type,
            };
            _context.StoreConceptAttributes.Add(entity);
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = 1;
        return opStatus;
    }
}

public class AttributeObject
{
    public string name { get; set; }
    public int type { get; set; }
}

public class AttributeEntity : AttributeObject
{
    public int conceptId { get; set; }
}

