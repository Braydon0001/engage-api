namespace Engage.Application.Services.ProductOrderTypes.Commands;

public class ProductOrderTypeInsertCommand : IMapTo<ProductOrderType>, IRequest<ProductOrderType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderTypeInsertCommand, ProductOrderType>();
    }
}

public record ProductOrderTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderTypeInsertCommand, ProductOrderType>
{
    public async Task<ProductOrderType> Handle(ProductOrderTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProductOrderTypeInsertCommand, ProductOrderType>(command);
        
        Context.ProductOrderTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductOrderTypeInsertValidator : AbstractValidator<ProductOrderTypeInsertCommand>
{
    public ProductOrderTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}