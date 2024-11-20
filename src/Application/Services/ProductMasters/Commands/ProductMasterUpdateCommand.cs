// auto-generated
namespace Engage.Application.Services.ProductMasters.Commands;

public class ProductMasterUpdateCommand : IMapTo<ProductMaster>, IRequest<ProductMaster>
{
    public int Id { get; set; }
    public int ProductBrandId { get; set; }
    public int ProductReasonId { get; set; }
    public int ProductSubCategoryId { get; set; }
    public int ProductMasterStatusId { get; set; }
    public int ProductMasterSystemStatusId { get; set; }
    public int ProductVendorId { get; set; }
    public int ProductManufacturerId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string LedgerCode { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterUpdateCommand, ProductMaster>();
    }
}

public class ProductMasterUpdateHandler : UpdateHandler, IRequestHandler<ProductMasterUpdateCommand, ProductMaster>
{
    public ProductMasterUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMaster> Handle(ProductMasterUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductMasters.SingleOrDefaultAsync(e => e.ProductMasterId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductMasterValidator : AbstractValidator<ProductMasterUpdateCommand>
{
    public UpdateProductMasterValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductBrandId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductReasonId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductSubCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductMasterStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductMasterSystemStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductVendorId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductManufacturerId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Code).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).NotEmpty().MaximumLength(100);
        RuleFor(e => e.LedgerCode).MaximumLength(100);
    }
}