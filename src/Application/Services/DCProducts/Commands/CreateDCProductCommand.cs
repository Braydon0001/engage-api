namespace Engage.Application.Services.DCProducts.Commands;

public class CreateDCProductCommand : DCProductCommand, IRequest<OperationStatus>
{
    public int EngageVariantProductId { get; set; }
}

public class CreateDCProductCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateDCProductCommand, OperationStatus>
{
    public CreateDCProductCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateDCProductCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateDCProductCommand, DCProduct>(command);
        entity.EngageVariantProductId = command.EngageVariantProductId;
        _context.DCProducts.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.DCProductId;
        return opStatus;
    }
}
