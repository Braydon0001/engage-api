namespace Engage.Application.Services.ProductOrderLines.Commands;

public class ProductOrderLineUpdateCommand : IMapTo<ProductOrderLine>, IRequest<ProductOrderLine>
{
    public int Id { get; set; }
    public int ProductOrderId { get; init; }
    public int ProductId { get; init; }
    public int ProductOrderLineStatusId { get; init; }
    public int ProductOrderLineTypeId { get; init; }
    public decimal Amount { get; init; }
    public float Quantity { get; init; }
    public string Note { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLineUpdateCommand, ProductOrderLine>();
    }
}

public record ProductOrderLineUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineUpdateCommand, ProductOrderLine>
{
    public async Task<ProductOrderLine> Handle(ProductOrderLineUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrderLines.SingleOrDefaultAsync(e => e.ProductOrderLineId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductOrderLineValidator : AbstractValidator<ProductOrderLineUpdateCommand>
{
    public UpdateProductOrderLineValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductOrderId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductOrderLineStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductOrderLineTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Amount).NotEmpty();
        RuleFor(e => e.Quantity).NotEmpty();
        RuleFor(e => e.Note).MaximumLength(220);
    }
}