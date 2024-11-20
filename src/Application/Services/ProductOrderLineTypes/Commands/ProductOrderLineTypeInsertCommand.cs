namespace Engage.Application.Services.ProductOrderLineTypes.Commands;

public class ProductOrderLineTypeInsertCommand : IMapTo<ProductOrderLineType>, IRequest<ProductOrderLineType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLineTypeInsertCommand, ProductOrderLineType>();
    }
}

public record ProductOrderLineTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineTypeInsertCommand, ProductOrderLineType>
{
    public async Task<ProductOrderLineType> Handle(ProductOrderLineTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProductOrderLineTypeInsertCommand, ProductOrderLineType>(command);
        
        Context.ProductOrderLineTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductOrderLineTypeInsertValidator : AbstractValidator<ProductOrderLineTypeInsertCommand>
{
    public ProductOrderLineTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}