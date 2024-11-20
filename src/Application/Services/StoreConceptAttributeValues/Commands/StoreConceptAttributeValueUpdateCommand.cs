namespace Engage.Application.Services.StoreConceptAttributeValues.Commands;

public class StoreConceptAttributeValueUpdateCommand : StoreConceptAttributeValueCommand, IRequest<OperationStatus>
{
}
public class StoreConceptAttributeValueUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<StoreConceptAttributeValueUpdateCommand, OperationStatus>
{
    public StoreConceptAttributeValueUpdateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeValueUpdateCommand request, CancellationToken cancellationToken)
    {
        var attributeTypeId = _context.StoreConceptAttributes.Where(e => e.StoreConceptAttributeId == request.StoreConceptAttributeId).Select(e => e.StoreConceptAttributeTypeId).FirstOrDefault();
        var attributeType = _context.StoreConceptAttributeTypes.Where(e => e.Id == attributeTypeId).Select(e => e.Name).FirstOrDefault();
        var entity = await _context.StoreConceptAttributeValues.SingleAsync(x => x.StoreConceptAttributeValueId == request.Id, cancellationToken);
        
        if (attributeType == "Options")
        {
            var attributeOptionId = _context.StoreConceptAttributeOptions.Where(e => e.StoreConceptAttributeId == request.StoreConceptAttributeId && e.Option == request.Value).Select(e => e.StoreConceptAttributeOptionId).FirstOrDefault();
            entity.StoreConceptAttributeOptionId = attributeOptionId;
        }
        
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreConceptAttributeValueId;
        return opStatus;
    }
}

