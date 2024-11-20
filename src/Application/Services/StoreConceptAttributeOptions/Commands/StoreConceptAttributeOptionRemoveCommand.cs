namespace Engage.Application.Services.StoreConceptAttributeOptions.Commands;

public class StoreConceptAttributeOptionRemoveCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class StoreConceptAttributeOptionRemoveCommandHandler : IRequestHandler<StoreConceptAttributeOptionRemoveCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;


    public StoreConceptAttributeOptionRemoveCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeOptionRemoveCommand request, CancellationToken cancellationToken)
    {
        var entries = await _context.StoreConceptAttributeValues.Where(e => e.StoreConceptAttributeOptionId == request.Id).ToListAsync();

        if (entries.Count == 0)
        {
            return await _mediator.Send(new DeleteCommand
            {
                EntityName = "StoreConceptAttributeOption",
                Id = request.Id,
            });
        }

        throw new Exception("Cannot Delete While Value Is In Use");
    }
}
