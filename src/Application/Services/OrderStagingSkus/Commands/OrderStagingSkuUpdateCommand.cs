namespace Engage.Application.Services.OrderStagingSkus.Commands;

public class OrderStagingSkuUpdateCommand : IMapTo<OrderStagingSku>, IRequest<OrderStagingSku>
{
    public int Id { get; set; }
    public int OrderStagingId { get; init; }
    public string ProductName { get; init; }
    public string Barcode { get; init; }
    public string UnitType { get; init; }
    public int Quantity { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderStagingSkuUpdateCommand, OrderStagingSku>();
    }
}

public record OrderStagingSkuUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrderStagingSkuUpdateCommand, OrderStagingSku>
{
    public async Task<OrderStagingSku> Handle(OrderStagingSkuUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.OrderStagingSkus.SingleOrDefaultAsync(e => e.OrderStagingSkuId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateOrderStagingSkuValidator : AbstractValidator<OrderStagingSkuUpdateCommand>
{
    public UpdateOrderStagingSkuValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.OrderStagingId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductName).MaximumLength(120);
        RuleFor(e => e.Barcode).MaximumLength(120);
        RuleFor(e => e.UnitType).MaximumLength(120);
        RuleFor(e => e.Quantity);
    }
}