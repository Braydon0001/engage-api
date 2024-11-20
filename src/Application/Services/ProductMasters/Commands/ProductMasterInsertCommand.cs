// auto-generated
namespace Engage.Application.Services.ProductMasters.Commands;

public class ProductMasterInsertCommand : IMapTo<ProductMaster>, IRequest<ProductMaster>
{
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
        profile.CreateMap<ProductMasterInsertCommand, ProductMaster>();
    }
}

public class ProductMasterInsertHandler : InsertHandler, IRequestHandler<ProductMasterInsertCommand, ProductMaster>
{
    public ProductMasterInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMaster> Handle(ProductMasterInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductMasterInsertCommand, ProductMaster>(command);

        _context.ProductMasters.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductMasterInsertValidator : AbstractValidator<ProductMasterInsertCommand>
{
    public ProductMasterInsertValidator()
    {
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