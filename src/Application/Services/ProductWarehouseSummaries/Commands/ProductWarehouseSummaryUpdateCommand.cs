// auto-generated
namespace Engage.Application.Services.ProductWarehouseSummaries.Commands;

public class ProductWarehouseSummaryUpdateCommand : IMapTo<ProductWarehouseSummary>, IRequest<ProductWarehouseSummary>
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ProductWarehouseId { get; set; }
    public int ProductPeriodId { get; set; }
    public float Quantity { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductWarehouseSummaryUpdateCommand, ProductWarehouseSummary>();
    }
}

public class ProductWarehouseSummaryUpdateHandler : UpdateHandler, IRequestHandler<ProductWarehouseSummaryUpdateCommand, ProductWarehouseSummary>
{
    public ProductWarehouseSummaryUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductWarehouseSummary> Handle(ProductWarehouseSummaryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductWarehouseSummaries.SingleOrDefaultAsync(e => e.ProductWarehouseSummaryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductWarehouseSummaryValidator : AbstractValidator<ProductWarehouseSummaryUpdateCommand>
{
    public UpdateProductWarehouseSummaryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductWarehouseId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductPeriodId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Quantity).NotEmpty();
    }
}