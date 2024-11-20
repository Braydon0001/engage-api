namespace Engage.Application.Services.StoreConceptAttributes.Commands;

public class StoreConceptAttributeCreateCommand : IRequest<OperationStatus>
{
    public string conceptName { get; set; }
    public List<AttributeObject> attributes { get; set; }
}
public class StoreConceptAttributeCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<StoreConceptAttributeCreateCommand, OperationStatus>
{
    public StoreConceptAttributeCreateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeCreateCommand request, CancellationToken cancellationToken)
    {
        var newConcept = new StoreConcept { Name = request.conceptName };
        _context.StoreConcepts.Add(newConcept);
        await _context.SaveChangesAsync(cancellationToken);
        var conceptId = newConcept.Id;

        foreach (var concept in request.attributes)
        {
            var entity = new StoreConceptAttribute
            {
                StoreConceptId = conceptId,
                Name = concept.name,
                StoreConceptAttributeTypeId = concept.type,
            };
            _context.StoreConceptAttributes.Add(entity);
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = conceptId;
        return opStatus;
    }
}



