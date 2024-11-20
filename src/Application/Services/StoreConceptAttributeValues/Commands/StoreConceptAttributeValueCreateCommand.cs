namespace Engage.Application.Services.StoreConceptAttributeValues.Commands;

public class StoreConceptAttributeValueCreateCommand : StoreConceptAttributeValueCommand, IRequest<OperationStatus>
{
}
public class StoreConceptAttributeValueCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<StoreConceptAttributeValueCreateCommand, OperationStatus>
{
    public StoreConceptAttributeValueCreateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeValueCreateCommand request, CancellationToken cancellationToken)
    {
        var attributeTypeId = _context.StoreConceptAttributes.Where(e => e.StoreConceptAttributeId == request.StoreConceptAttributeId).Select(e => e.StoreConceptAttributeTypeId).FirstOrDefault();
        var attributeType = _context.StoreConceptAttributeTypes.Where(e => e.Id == attributeTypeId).Select(e => e.Name).FirstOrDefault();
        var entity = _mapper.Map<StoreConceptAttributeValueCreateCommand, StoreConceptAttributeValue>(request);
        if (attributeType == "Options")
        {
            var attributeOptionId = _context.StoreConceptAttributeOptions.Where(e => e.StoreConceptAttributeId == request.StoreConceptAttributeId && e.Option == request.Value).Select(e => e.StoreConceptAttributeOptionId).FirstOrDefault();
            entity.StoreConceptAttributeOptionId = attributeOptionId;
        }

        _context.StoreConceptAttributeValues.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreConceptAttributeValueId;
        return opStatus;
    }
}

