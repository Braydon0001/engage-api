namespace Engage.Application.Services.ProductPrices.Commands;

public class ProductPriceInsertCommand : IMapTo<ProductPrice>, IRequest<ProductPrice>
{
    public int ProductId { get; init; }
    public DateTime StartDate { get; set; }
    public decimal Price { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductPriceInsertCommand, ProductPrice>();
    }
}

public record ProductPriceInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductPriceInsertCommand, ProductPrice>
{
    public async Task<ProductPrice> Handle(ProductPriceInsertCommand command, CancellationToken cancellationToken)
    {
        command.StartDate = DateTime.Now;
        var entity = Mapper.Map<ProductPriceInsertCommand, ProductPrice>(command);

        Context.ProductPrices.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductPriceInsertValidator : AbstractValidator<ProductPriceInsertCommand>
{
    public ProductPriceInsertValidator()
    {
        RuleFor(e => e.ProductId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.Price).NotEmpty();
    }
}