namespace Engage.Application.Services.OrderTemplateProducts.Commands;

public class OrderTemplateProductGrossProfitPercentUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public decimal GrossProfitPercent { get; set; }
}

public class OrderTemplateProductGrossProfitPercentUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderTemplateProductGrossProfitPercentUpdateCommand, OperationStatus>
{
    public OrderTemplateProductGrossProfitPercentUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderTemplateProductGrossProfitPercentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplateProducts.SingleAsync(e => e.OrderTemplateProductId == command.Id, cancellationToken);
        entity.GrossProfitPercent = command.GrossProfitPercent;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.OrderTemplateProductId;
        return operationStatus;
    }
}

public class OrderTemplateProductGrossProfitPercentUpdateValidator : AbstractValidator<OrderTemplateProductGrossProfitPercentUpdateCommand>
{
    public OrderTemplateProductGrossProfitPercentUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.GrossProfitPercent).GreaterThanOrEqualTo(0);
    }
}