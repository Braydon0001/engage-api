namespace Engage.Application.Services.StoreConceptAttributes.Commands;

public class StoreConceptAttributeRemoveCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class StoreConceptAttributeRemoveCommandHandler : IRequestHandler<StoreConceptAttributeRemoveCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;


    public StoreConceptAttributeRemoveCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeRemoveCommand request, CancellationToken cancellationToken)
    {
        var entries = await _context.StoreConceptAttributeValues.Where(e => e.StoreConceptAttributeId == request.Id).ToListAsync();

        var hasOptions = await _context.StoreConceptAttributeOptions.Where(e => e.StoreConceptAttributeId == request.Id).ToListAsync();

        if (entries.Count == 0 && hasOptions.Count == 0)
        {
            return await _mediator.Send(new DeleteCommand
            {
                EntityName = "StoreConceptAttribute",
                Id = request.Id,
            });
        }

        throw new Exception("Cannot Delete While Value Is In Use");
    }
}
