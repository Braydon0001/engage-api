namespace Engage.Application.Services.StoreConceptAttributes.Commands;

public class StoreConceptAttributeUpdateCommand : StoreConceptAttributeCommand, IRequest<OperationStatus>
{
}
public class StoreConceptAttributeUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<StoreConceptAttributeUpdateCommand, OperationStatus>
{
    public StoreConceptAttributeUpdateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeUpdateCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.StoreConceptAttributes.SingleAsync(x => x.StoreConceptAttributeId == request.Id, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreConceptAttributeId;
        return opStatus;
    }
}

