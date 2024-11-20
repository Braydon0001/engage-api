namespace Engage.Application.Services.StoreConceptAttributeOptions.Commands;

public class StoreConceptAttributeOptionBatchCreateCommand : IRequest<OperationStatus>
{
    public int id { get; set; }
    public List<AttributeOptionObject> options { get; set; }
}
public class StoreConceptAttributeOptionBatchCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<StoreConceptAttributeOptionBatchCreateCommand, OperationStatus>
{
    public StoreConceptAttributeOptionBatchCreateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeOptionBatchCreateCommand request, CancellationToken cancellationToken)
    {
        foreach (var option in request.options)
        {
            var entity = new StoreConceptAttributeOption
            {
                StoreConceptAttributeId = request.id,
                Option = option.name,
            };
            _context.StoreConceptAttributeOptions.Add(entity);
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = 1;
        return opStatus;
    }
}

public class AttributeOptionObject
{
    public string name { get; set; }
}

