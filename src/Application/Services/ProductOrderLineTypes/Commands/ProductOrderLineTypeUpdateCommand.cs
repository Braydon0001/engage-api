namespace Engage.Application.Services.ProductOrderLineTypes.Commands;

public class ProductOrderLineTypeUpdateCommand : IMapTo<ProductOrderLineType>, IRequest<ProductOrderLineType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLineTypeUpdateCommand, ProductOrderLineType>();
    }
}

public record ProductOrderLineTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineTypeUpdateCommand, ProductOrderLineType>
{
    public async Task<ProductOrderLineType> Handle(ProductOrderLineTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrderLineTypes.SingleOrDefaultAsync(e => e.ProductOrderLineTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductOrderLineTypeValidator : AbstractValidator<ProductOrderLineTypeUpdateCommand>
{
    public UpdateProductOrderLineTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}