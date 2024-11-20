namespace Engage.Application.Services.ProductOrderLines.Commands;

public class ProductOrderLineInsertCommand : IMapTo<ProductOrderLine>, IRequest<ProductOrderLine>
{
    public int ProductOrderId { get; init; }
    public int ProductId { get; init; }
    public int ProductOrderLineStatusId { get; init; }
    public int ProductOrderLineTypeId { get; init; }
    public decimal Amount { get; init; }
    public float Quantity { get; init; }
    public string Note { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLineInsertCommand, ProductOrderLine>();
    }
}

public record ProductOrderLineInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineInsertCommand, ProductOrderLine>
{
    public async Task<ProductOrderLine> Handle(ProductOrderLineInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProductOrderLineInsertCommand, ProductOrderLine>(command);
        
        Context.ProductOrderLines.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductOrderLineInsertValidator : AbstractValidator<ProductOrderLineInsertCommand>
{
    public ProductOrderLineInsertValidator()
    {
        RuleFor(e => e.ProductOrderId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductOrderLineStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductOrderLineTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Amount).NotEmpty();
        RuleFor(e => e.Quantity).NotEmpty();
        RuleFor(e => e.Note).MaximumLength(220);
    }
}