namespace Engage.Application.Services.StoreConceptAttributeValues.Commands;

public class StoreConceptAttributeValueBulkUpdateCommand : IRequest<OperationStatus>
{
    public int id { get; set; }
    public int conceptId { get; set; }
    public List<AttributeValue> flatAttributes { get; set; }
}
public class StoreConceptAttributeValueBulkUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<StoreConceptAttributeValueBulkUpdateCommand, OperationStatus>
{
    public StoreConceptAttributeValueBulkUpdateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeValueBulkUpdateCommand request, CancellationToken cancellationToken)
    {
        var storeId = request.id;
        var conceptId = request.conceptId;
        var attributeValues = request.flatAttributes;

        var conceptAttributes = _context.StoreConceptAttributes.Where(e => e.StoreConceptId == conceptId).ToList();



        foreach (var attribute in conceptAttributes)
        {
            var dbAttributeValue = _context.StoreConceptAttributeValues.Where(e => e.StoreId == storeId && e.StoreConceptAttributeId == attribute.StoreConceptAttributeId).FirstOrDefault(); //get the value from the db
            var newValue = attributeValues.Where(e => e.Name == attribute.Name).Select(e => e.Value).FirstOrDefault(); //get the new value

            var storeAssets = await _context.StoreConceptAttributeStoreAssets.Where(e => e.StoreConceptAttributeId == attribute.StoreConceptAttributeId).ToListAsync();

            if (dbAttributeValue != null && newValue != null && dbAttributeValue.Value != newValue) //if the value exists and there is a new value, update it
            {
                await _mediator.Send(new StoreConceptAttributeValueUpdateCommand
                {
                    Id = dbAttributeValue.StoreConceptAttributeValueId,
                    StoreId = storeId,
                    StoreConceptAttributeId = attribute.StoreConceptAttributeId,
                    Value = newValue,
                });
            }
            else if (dbAttributeValue == null && newValue != null) //if the value does not exist, but there is a new value, create it
            {
                await _mediator.Send(new StoreConceptAttributeValueCreateCommand
                {
                    StoreId = storeId,
                    StoreConceptAttributeId = attribute.StoreConceptAttributeId,
                    Value = newValue,
                });
            }
            else if (dbAttributeValue != null && newValue == null) //if the value exists but there is not a new value, delete it
            {
                await _mediator.Send(new StoreConceptAttributeValueRemoveCommand
                {
                    Id = dbAttributeValue.StoreConceptAttributeValueId,
                });

                foreach (var asset in storeAssets)
                {
                    _context.StoreConceptAttributeStoreAssets.Remove(asset);
                }
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}

public class AttributeValue
{
    public string Name { get; set; }
    public string Value { get; set; }
}

