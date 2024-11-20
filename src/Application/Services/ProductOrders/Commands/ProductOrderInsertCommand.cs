namespace Engage.Application.Services.ProductOrders.Commands;

public class ProductOrderInsertCommand : IMapTo<ProductOrder>, IRequest<ProductOrder>
{
    public string OrderNumber { get; init; }
    public int ProductOrderStatusId { get; init; }
    public int ProductWarehouseId { get; init; }
    public int? ProductWarehouseOutId { get; init; }
    public int ProductOrderTypeId { get; init; }
    public int ProductPeriodId { get; init; }
    public int ProductSupplierId { get; init; }
    public DateTime OrderDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderInsertCommand, ProductOrder>();
    }
}

public record ProductOrderInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderInsertCommand, ProductOrder>
{
    public async Task<ProductOrder> Handle(ProductOrderInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProductOrderInsertCommand, ProductOrder>(command);

        var previousOrder = await Context.ProductOrders
                                    .FirstOrDefaultAsync(e => e.OrderNumber == command.OrderNumber
                                        && e.OrderDate.Date == command.OrderDate.Date, cancellationToken);
        if (previousOrder != null)
        {
            throw new Exception("A purchase order with this order number and date already exists.");
        }

        if (command.ProductOrderTypeId == (int)ProductOrderTypeEnum.Transfer && command.ProductWarehouseOutId == null)
        {
            throw new Exception("No outgoing warehouse found");
        }

        entity.ProductOrderStatusId = 1; //Set status to unsubmitted

        var productPeriod = await Context.ProductPeriods
                            .FirstOrDefaultAsync(e => e.StartDate.Date <= command.OrderDate.Date
                                && e.EndDate.Date >= command.OrderDate.Date, cancellationToken) ?? throw new Exception("Inventory Period not found.");

        entity.ProductPeriodId = productPeriod.ProductPeriodId;

        Context.ProductOrders.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductOrderInsertValidator : AbstractValidator<ProductOrderInsertCommand>
{
    public ProductOrderInsertValidator()
    {
        RuleFor(e => e.OrderNumber).MaximumLength(200);
        RuleFor(e => e.ProductOrderStatusId);
        RuleFor(e => e.ProductOrderTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductWarehouseId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductWarehouseOutId);
        RuleFor(e => e.ProductOrderTypeId);
        RuleFor(e => e.ProductPeriodId);
        RuleFor(e => e.ProductSupplierId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.OrderDate).NotEmpty();
    }
}