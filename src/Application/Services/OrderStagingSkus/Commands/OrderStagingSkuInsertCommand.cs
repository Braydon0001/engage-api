namespace Engage.Application.Services.OrderStagingSkus.Commands;

public class OrderStagingSkuInsertCommand : IMapTo<OrderStagingSku>, IRequest<OrderStagingSku>
{
    public int OrderStagingId { get; init; }
    public string ProductName { get; init; }
    public string Barcode { get; init; }
    public string UnitType { get; init; }
    public int Quantity { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderStagingSkuInsertCommand, OrderStagingSku>();
    }
}

public record OrderStagingSkuInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrderStagingSkuInsertCommand, OrderStagingSku>
{
    public async Task<OrderStagingSku> Handle(OrderStagingSkuInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<OrderStagingSkuInsertCommand, OrderStagingSku>(command);
        
        Context.OrderStagingSkus.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderStagingSkuInsertValidator : AbstractValidator<OrderStagingSkuInsertCommand>
{
    public OrderStagingSkuInsertValidator()
    {
        RuleFor(e => e.OrderStagingId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductName).MaximumLength(120);
        RuleFor(e => e.Barcode).MaximumLength(120);
        RuleFor(e => e.UnitType).MaximumLength(120);
        RuleFor(e => e.Quantity);
    }
}