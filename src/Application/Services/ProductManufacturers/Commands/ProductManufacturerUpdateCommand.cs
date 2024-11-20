// auto-generated
namespace Engage.Application.Services.ProductManufacturers.Commands;

public class ProductManufacturerUpdateCommand : IMapTo<ProductManufacturer>, IRequest<ProductManufacturer>
{
    public int Id { get; set; }
    public int ProductSupplierId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductManufacturerUpdateCommand, ProductManufacturer>();
    }
}

public class ProductManufacturerUpdateHandler : UpdateHandler, IRequestHandler<ProductManufacturerUpdateCommand, ProductManufacturer>
{
    public ProductManufacturerUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductManufacturer> Handle(ProductManufacturerUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductManufacturers.SingleOrDefaultAsync(e => e.ProductManufacturerId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductManufacturerValidator : AbstractValidator<ProductManufacturerUpdateCommand>
{
    public UpdateProductManufacturerValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductSupplierId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Code).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}