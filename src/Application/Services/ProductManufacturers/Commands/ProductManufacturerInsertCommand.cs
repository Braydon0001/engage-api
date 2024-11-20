// auto-generated
namespace Engage.Application.Services.ProductManufacturers.Commands;

public class ProductManufacturerInsertCommand : IMapTo<ProductManufacturer>, IRequest<ProductManufacturer>
{
    public int ProductSupplierId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductManufacturerInsertCommand, ProductManufacturer>();
    }
}

public class ProductManufacturerInsertHandler : InsertHandler, IRequestHandler<ProductManufacturerInsertCommand, ProductManufacturer>
{
    public ProductManufacturerInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductManufacturer> Handle(ProductManufacturerInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductManufacturerInsertCommand, ProductManufacturer>(command);
        
        _context.ProductManufacturers.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductManufacturerInsertValidator : AbstractValidator<ProductManufacturerInsertCommand>
{
    public ProductManufacturerInsertValidator()
    {
        RuleFor(e => e.ProductSupplierId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Code).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}