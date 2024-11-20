namespace Engage.Application.Services.ProductOrders.Commands;

public class ProductOrderWarehouseUpdateCommand : IRequest<ProductOrder>
{
    public int Id { get; set; }
    public int ProductWarehouseId { get; set; }
}
public record ProductOrderWarehouseUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderWarehouseUpdateCommand, ProductOrder>
{
    public async Task<ProductOrder> Handle(ProductOrderWarehouseUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrders.Where(e => e.ProductOrderId == command.Id).FirstOrDefaultAsync(cancellationToken)
                            ?? throw new Exception("Order not found");

        entity.ProductWarehouseId = command.ProductWarehouseId;

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
public class ProductOrderWarehouseUpdateValidator : AbstractValidator<ProductOrderWarehouseUpdateCommand>
{
    public ProductOrderWarehouseUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductWarehouseId).NotEmpty();
    }
}