namespace Engage.Application.Services.StoreConceptAttributeOptions.Commands;

public class StoreConceptAttributeOptionCreateCommand : StoreConceptAttributeOptionCommand, IRequest<OperationStatus>
{
}
public class StoreConceptAttributeOptionCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<StoreConceptAttributeOptionCreateCommand, OperationStatus>
{
    public StoreConceptAttributeOptionCreateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeOptionCreateCommand request, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<StoreConceptAttributeOptionCreateCommand, StoreConceptAttributeOption>(request);
        _context.StoreConceptAttributeOptions.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreConceptAttributeOptionId;
        return opStatus;
    }
}

