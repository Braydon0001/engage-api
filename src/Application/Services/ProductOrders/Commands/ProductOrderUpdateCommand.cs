namespace Engage.Application.Services.ProductOrders.Commands;

public class ProductOrderUpdateCommand : IMapTo<ProductOrder>, IRequest<ProductOrder>
{
    public int Id { get; set; }
    public string OrderNumber { get; init; }
    public int ProductOrderStatusId { get; init; }
    public int ProductWarehouseId { get; init; }
    public int? ProductWarehouseOutId { get; init; }
    public int ProductOrderTypeId { get; init; }
    public int ProductPeriodId { get; init; }
    public int ProductSupplierId { get; init; }
    public DateTime OrderDate { get; init; }
    public List<JsonText> Note { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderUpdateCommand, ProductOrder>();
    }
}

public record ProductOrderUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderUpdateCommand, ProductOrder>
{
    public async Task<ProductOrder> Handle(ProductOrderUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrders.SingleOrDefaultAsync(e => e.ProductOrderId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }
        if (entity.ProductOrderStatusId == 2)
        {
            return null;
        }

        entity.Note = null;

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductOrderValidator : AbstractValidator<ProductOrderUpdateCommand>
{
    public UpdateProductOrderValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.OrderNumber).MaximumLength(200);
        RuleFor(e => e.ProductOrderStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductWarehouseId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductOrderTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductPeriodId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductSupplierId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.OrderDate).NotEmpty();
        RuleFor(e => e.Note);
    }
}