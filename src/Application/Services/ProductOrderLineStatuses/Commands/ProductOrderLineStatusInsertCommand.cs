namespace Engage.Application.Services.ProductOrderLineStatuses.Commands;

public class ProductOrderLineStatusInsertCommand : IMapTo<ProductOrderLineStatus>, IRequest<ProductOrderLineStatus>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLineStatusInsertCommand, ProductOrderLineStatus>();
    }
}

public record ProductOrderLineStatusInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineStatusInsertCommand, ProductOrderLineStatus>
{
    public async Task<ProductOrderLineStatus> Handle(ProductOrderLineStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProductOrderLineStatusInsertCommand, ProductOrderLineStatus>(command);
        
        Context.ProductOrderLineStatuses.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductOrderLineStatusInsertValidator : AbstractValidator<ProductOrderLineStatusInsertCommand>
{
    public ProductOrderLineStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}