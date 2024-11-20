// auto-generated
namespace Engage.Application.Services.ProductWarehouseSummaries.Commands;

public class ProductWarehouseSummaryInsertCommand : IMapTo<ProductWarehouseSummary>, IRequest<ProductWarehouseSummary>
{
    public int ProductId { get; set; }
    public int ProductWarehouseId { get; set; }
    public int ProductPeriodId { get; set; }
    public float Quantity { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductWarehouseSummaryInsertCommand, ProductWarehouseSummary>();
    }
}

public class ProductWarehouseSummaryInsertHandler : InsertHandler, IRequestHandler<ProductWarehouseSummaryInsertCommand, ProductWarehouseSummary>
{
    public ProductWarehouseSummaryInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductWarehouseSummary> Handle(ProductWarehouseSummaryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductWarehouseSummaryInsertCommand, ProductWarehouseSummary>(command);
        
        _context.ProductWarehouseSummaries.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductWarehouseSummaryInsertValidator : AbstractValidator<ProductWarehouseSummaryInsertCommand>
{
    public ProductWarehouseSummaryInsertValidator()
    {
        RuleFor(e => e.ProductId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductWarehouseId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductPeriodId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Quantity).NotEmpty();
    }
}