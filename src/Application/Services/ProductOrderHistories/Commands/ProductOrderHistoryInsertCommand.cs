namespace Engage.Application.Services.ProductOrderHistories.Commands;

public class ProductOrderHistoryInsertCommand : IMapTo<ProductOrderHistory>, IRequest<ProductOrderHistory>
{
    public int ProductOrderId { get; init; }
    public int ProductOrderStatusId { get; init; }
    public string Reason { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderHistoryInsertCommand, ProductOrderHistory>();
    }
}

public record ProductOrderHistoryInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderHistoryInsertCommand, ProductOrderHistory>
{
    public async Task<ProductOrderHistory> Handle(ProductOrderHistoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProductOrderHistoryInsertCommand, ProductOrderHistory>(command);
        
        Context.ProductOrderHistories.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductOrderHistoryInsertValidator : AbstractValidator<ProductOrderHistoryInsertCommand>
{
    public ProductOrderHistoryInsertValidator()
    {
        RuleFor(e => e.ProductOrderId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductOrderStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Reason).MaximumLength(120);
    }
}