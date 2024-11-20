namespace Engage.Application.Services.EngageVariantProducts.Commands;

public class CreateEngageVariantProductCommand : EngageVariantProductCommand, IRequest<OperationStatus>
{
    public int EngageMasterProductId { get; set; }
}

public class CreateEngageVariantProductCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEngageVariantProductCommand, OperationStatus>
{
    public CreateEngageVariantProductCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateEngageVariantProductCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEngageVariantProductCommand, EngageVariantProduct>(command);
        entity.EngageMasterProductId = command.EngageMasterProductId;
        _context.EngageVariantProducts.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EngageVariantProductId;
        return opStatus;
    }
}
