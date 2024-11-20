namespace Engage.Application.Services.OrderTemplateProducts.Commands;

public class OrderTemplateProductPriceUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public decimal Price { get; set; }
}

public class OrderTemplateProductPriceUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderTemplateProductPriceUpdateCommand, OperationStatus>
{
    public OrderTemplateProductPriceUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderTemplateProductPriceUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplateProducts.SingleAsync(e => e.OrderTemplateProductId == command.Id, cancellationToken);
        entity.Price = command.Price;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.OrderTemplateProductId;
        return operationStatus;
    }
}

public class OrderTemplateProductPriceUpdateValidator : AbstractValidator<OrderTemplateProductPriceUpdateCommand>
{
    public OrderTemplateProductPriceUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
    }
}