namespace Engage.Application.Services.ProductOrderTypes.Commands;

public class ProductOrderTypeUpdateCommand : IMapTo<ProductOrderType>, IRequest<ProductOrderType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderTypeUpdateCommand, ProductOrderType>();
    }
}

public record ProductOrderTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderTypeUpdateCommand, ProductOrderType>
{
    public async Task<ProductOrderType> Handle(ProductOrderTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrderTypes.SingleOrDefaultAsync(e => e.ProductOrderTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductOrderTypeValidator : AbstractValidator<ProductOrderTypeUpdateCommand>
{
    public UpdateProductOrderTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}