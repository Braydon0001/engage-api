// auto-generated
namespace Engage.Application.Services.Products.Commands;

public class ProductUpdateCommand : IMapTo<Product>, IRequest<Product>
{
    public int Id { get; set; }
    public int ProductMasterId { get; set; }
    public int? RelatedProductId { get; set; }
    public int ProductWarehouseId { get; set; }
    public int ProductSizeTypeId { get; set; }
    public int ProductPackSizeTypeId { get; set; }
    public int ProductModuleStatusId { get; set; }
    public int ProductSystemStatusId { get; set; }
    public int? ProductMasterColorId { get; set; }
    public int? ProductMasterSizeId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public float ProductSize { get; set; }
    public float ProductPackSize { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductUpdateCommand, Product>();
    }
}

public class ProductUpdateHandler : UpdateHandler, IRequestHandler<ProductUpdateCommand, Product>
{
    public ProductUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<Product> Handle(ProductUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.SingleOrDefaultAsync(e => e.ProductId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductValidator : AbstractValidator<ProductUpdateCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductMasterId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.RelatedProductId).GreaterThan(0);
        RuleFor(e => e.ProductWarehouseId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductSizeTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductPackSizeTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductModuleStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductSystemStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductMasterColorId);
        RuleFor(e => e.ProductMasterSizeId);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Code).MaximumLength(100);
        RuleFor(e => e.Description).MaximumLength(200);
        RuleFor(e => e.ProductSize).NotEmpty();
        RuleFor(e => e.ProductPackSize).NotEmpty();
    }
}