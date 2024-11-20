// auto-generated
namespace Engage.Application.Services.ProductSizeTypes.Commands;

public class ProductSizeTypeUpdateCommand : IMapTo<ProductSizeType>, IRequest<ProductSizeType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSizeTypeUpdateCommand, ProductSizeType>();
    }
}

public class ProductSizeTypeUpdateHandler : UpdateHandler, IRequestHandler<ProductSizeTypeUpdateCommand, ProductSizeType>
{
    public ProductSizeTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSizeType> Handle(ProductSizeTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductSizeTypes.SingleOrDefaultAsync(e => e.ProductSizeTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductSizeTypeValidator : AbstractValidator<ProductSizeTypeUpdateCommand>
{
    public UpdateProductSizeTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}