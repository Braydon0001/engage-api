namespace Engage.Application.Services.ProductOrderStatuses.Commands;

public class ProductOrderStatusInsertCommand : IMapTo<ProductOrderStatus>, IRequest<ProductOrderStatus>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderStatusInsertCommand, ProductOrderStatus>();
    }
}

public record ProductOrderStatusInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderStatusInsertCommand, ProductOrderStatus>
{
    public async Task<ProductOrderStatus> Handle(ProductOrderStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProductOrderStatusInsertCommand, ProductOrderStatus>(command);
        
        Context.ProductOrderStatuses.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductOrderStatusInsertValidator : AbstractValidator<ProductOrderStatusInsertCommand>
{
    public ProductOrderStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}