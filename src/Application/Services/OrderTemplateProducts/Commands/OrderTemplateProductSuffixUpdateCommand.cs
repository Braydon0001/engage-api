namespace Engage.Application.Services.OrderTemplateProducts.Commands;

public class OrderTemplateProductSuffixUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string Suffix { get; set; }
}

public class OrderTemplateProductSuffixUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderTemplateProductSuffixUpdateCommand, OperationStatus>
{
    public OrderTemplateProductSuffixUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderTemplateProductSuffixUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplateProducts.SingleAsync(e => e.OrderTemplateProductId == command.Id, cancellationToken);
        entity.Suffix = command.Suffix;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.OrderTemplateProductId;
        return operationStatus;
    }
}

public class OrderTemplateProductSuffixUpdateValidator : AbstractValidator<OrderTemplateProductSuffixUpdateCommand>
{
    public OrderTemplateProductSuffixUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Suffix).MaximumLength(100);
    }
}