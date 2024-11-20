namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuSuffixUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string Suffix { get; set; }
}

public class OrderSkuSuffixUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderSkuSuffixUpdateCommand, OperationStatus>
{
    public OrderSkuSuffixUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderSkuSuffixUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderSkus.SingleAsync(e => e.OrderSkuId == command.Id, cancellationToken);
        entity.Suffix = command.Suffix;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.OrderSkuId;
        return operationStatus;
    }
}

public class OrderSkuSuffixUpdateValidator : AbstractValidator<OrderSkuSuffixUpdateCommand>
{
    public OrderSkuSuffixUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Suffix).MaximumLength(100);
    }
}