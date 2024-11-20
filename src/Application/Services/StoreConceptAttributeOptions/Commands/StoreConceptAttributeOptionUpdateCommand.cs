namespace Engage.Application.Services.StoreConceptAttributeOptions.Commands;

public class StoreConceptAttributeOptionUpdateCommand : StoreConceptAttributeOptionCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}
public class StoreConceptAttributeOptionUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<StoreConceptAttributeOptionUpdateCommand, OperationStatus>
{
    public StoreConceptAttributeOptionUpdateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeOptionUpdateCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.StoreConceptAttributeOptions.SingleAsync(x => x.StoreConceptAttributeOptionId == request.Id, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreConceptAttributeOptionId;
        return opStatus;
    }
}

