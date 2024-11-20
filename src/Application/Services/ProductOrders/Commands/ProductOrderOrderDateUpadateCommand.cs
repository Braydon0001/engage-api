namespace Engage.Application.Services.ProductOrders.Commands;

public class ProductOrderOrderDateUpadateCommand : IRequest<ProductOrder>
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
}
public record ProductOrderOrderDateUpadateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderOrderDateUpadateCommand, ProductOrder>
{
    public async Task<ProductOrder> Handle(ProductOrderOrderDateUpadateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrders.Where(e => e.ProductOrderId == command.Id).FirstOrDefaultAsync(cancellationToken)
                            ?? throw new Exception("Order not found");

        entity.OrderDate = command.OrderDate;

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
public class ProductOrderOrderDateUpadateValidator : AbstractValidator<ProductOrderOrderDateUpadateCommand>
{
    public ProductOrderOrderDateUpadateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.OrderDate).NotEmpty();
    }
}