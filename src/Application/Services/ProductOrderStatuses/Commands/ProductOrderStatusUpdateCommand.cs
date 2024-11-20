namespace Engage.Application.Services.ProductOrderStatuses.Commands;

public class ProductOrderStatusUpdateCommand : IMapTo<ProductOrderStatus>, IRequest<ProductOrderStatus>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderStatusUpdateCommand, ProductOrderStatus>();
    }
}

public record ProductOrderStatusUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderStatusUpdateCommand, ProductOrderStatus>
{
    public async Task<ProductOrderStatus> Handle(ProductOrderStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrderStatuses.SingleOrDefaultAsync(e => e.ProductOrderStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductOrderStatusValidator : AbstractValidator<ProductOrderStatusUpdateCommand>
{
    public UpdateProductOrderStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}