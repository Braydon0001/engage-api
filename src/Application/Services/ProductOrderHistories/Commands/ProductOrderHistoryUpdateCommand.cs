namespace Engage.Application.Services.ProductOrderHistories.Commands;

public class ProductOrderHistoryUpdateCommand : IMapTo<ProductOrderHistory>, IRequest<ProductOrderHistory>
{
    public int Id { get; set; }
    public int ProductOrderId { get; init; }
    public int ProductOrderStatusId { get; init; }
    public string Reason { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderHistoryUpdateCommand, ProductOrderHistory>();
    }
}

public record ProductOrderHistoryUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderHistoryUpdateCommand, ProductOrderHistory>
{
    public async Task<ProductOrderHistory> Handle(ProductOrderHistoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrderHistories.SingleOrDefaultAsync(e => e.ProductOrderHistoryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductOrderHistoryValidator : AbstractValidator<ProductOrderHistoryUpdateCommand>
{
    public UpdateProductOrderHistoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductOrderId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductOrderStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Reason).MaximumLength(120);
    }
}